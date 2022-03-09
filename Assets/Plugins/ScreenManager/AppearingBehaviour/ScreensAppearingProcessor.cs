using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ScreenManager.Events;
using ScreenManager.Extensions;
using ScreenManager.Interfaces;
using SimpleBus.Extensions;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.Assertions;

namespace ScreenManager
{
    /// <summary>
    /// This class controls appearing new screens and disappearing old ones.
    /// If it's in progress, it will put the request in the queue and will process it on the finish.
    /// </summary>
    public class ScreensAppearingProcessor : IDisposable, IAppearingRequestsHolder
    {
        public Queue<AppearingScreensRequest> Requests { get; } = new Queue<AppearingScreensRequest>();

        private readonly ScreenAppearingRequestDebugInformation _lastRequestInformation = new ScreenAppearingRequestDebugInformation();
        private readonly MonoBehaviour _holder;
        private readonly ObjectPool<List<ScreenData>> _listsPool;
        private readonly ScreensCache _screensCache;
        private readonly IDisposable _subscriptions;
        private bool _isInProgress;
        private AppearingScreensRequest _currentRequest;
        private Coroutine _showScreensCoroutine;

        public ScreensAppearingProcessor(MonoBehaviour holder,
            ScreensCache screensCache,
            ObjectPool<List<ScreenData>> pool)
        {
            _screensCache = screensCache;
            _listsPool = pool;
            _holder = holder;
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<GetScreenDebugDataEvent>(GetScreenDebugDataHandler),
            };
        }

        private void GetScreenDebugDataHandler(GetScreenDebugDataEvent debugData)
        {
            debugData.IsAppearingProcessorWorking = _isInProgress;
            debugData.LastRequestInformation = _lastRequestInformation;
            debugData.AppearingScreensRequests = Requests;
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        public void TryProcessNext()
        {
            if (Requests.Count == 0)
            {
                return;
            }
            
            if (HasNotLoadedScreen(Requests.Peek().ScreensToShow))
            {
                Debug.Log("Cannot process next screen, it has not loaded screens. Start waiting...");
                return;
            }
            
            if (!_isInProgress)
            {
                var screensItem = Requests.Dequeue();
                Process(screensItem.ScreensToShow, screensItem.ScreensToHide, screensItem.ScreensToUnload);
            }
        }

        public void AddToQueueAndTryProcess([NotNull] List<ScreenData> show, [NotNull] List<ScreenData> hide, [NotNull] List<ScreenData> unload)
        {
            Requests.Enqueue(new AppearingScreensRequest(show, hide, unload));
            TryProcessNext();
        }

        private void Process([NotNull] List<ScreenData> show, [NotNull] List<ScreenData> hide, [NotNull] List<ScreenData> unload)
        {
            Assert.IsNotNull(show);
            Assert.IsNotNull(hide);
            Assert.IsNotNull(unload);
            
            if (_isInProgress || HasNotLoadedScreen(show))
            {
                Requests.Enqueue(new AppearingScreensRequest(show, hide, unload));
                return;
            }
            
            _currentRequest = new AppearingScreensRequest(show, hide, unload);
            _lastRequestInformation.SetNewRequest(_currentRequest);
            _showScreensCoroutine = _holder.StartCoroutine(ShowScreens(show, hide, unload));
        }

        private bool HasNotLoadedScreen([NotNull] List<ScreenData> screens)
        {
            for (var i = 0; i < screens.Count; i++)
            {
                if (!_screensCache.IsLoaded(screens[i].Id))
                {
                    Debug.Log($"Screen {screens[i].Id} is waiting for the scene.");
                    return true;
                }
            }

            return false;
        }

        private IEnumerator ShowScreens(List<ScreenData> screensToShow, List<ScreenData> screensToHide, List<ScreenData> screensToUnload)
        {
            _isInProgress = true;
            
            yield return HideScreens(screensToUnload);
            _lastRequestInformation.MarkUnloadScreensProcessed();
            
            yield return HideScreens(screensToHide);
            _lastRequestInformation.MarkHideScreensProcessed();

            yield return ShowScreens(screensToShow);
            _lastRequestInformation.MarkShowScreensProcessed();

            _listsPool.Release(screensToHide);
            _listsPool.Release(screensToShow);

            UnloadAndReleaseProvidedList(screensToUnload);

            _isInProgress = false;

            TryProcessNext();
        }

        private void UnloadAndReleaseProvidedList(List<ScreenData> screensToUnload)
        {
            foreach (var screen in screensToUnload)
            {
                _screensCache.Unload(screen.Id);
                ScreenUnloadedEvent.Create(screen).Publish(EventStreams.UserInterface);
            }

            _listsPool.Release(screensToUnload);
        }

        private IEnumerator ShowScreens(List<ScreenData> screensToShow)
        {
            List<ScreenData> screensCopy = new List<ScreenData>(screensToShow);
            foreach (var screenData in screensCopy)
            {
                if (!_screensCache.IsLoaded(screenData.Id))
                {
                    Debug.LogError($"Bug: Cannot show not loaded screen ({screenData.Id}). It was skipped.");
                    continue;
                }

                var screen = _screensCache.GetScreen(screenData.Id);
                Assert.IsTrue(screen.State.CurrentValue.IsOneOf(ScreenState.Shown, ScreenState.Hidden, ScreenState.Unknown));

                try
                {
                    screen.Guid = screenData.ScreenGuid;
                    if (screen.State.CurrentValue.IsOneOf(ScreenState.Hidden, ScreenState.Unknown))
                    {
                        screen.Initialize(screenData.Context);
                    }
                    screen.SetDrawingOrder(screenData.StackPosition);
                }
                catch (Exception exception)
                {
                    HandleExceptionAndRemoveScreenEverywhere(screenData, exception);
                    continue;
                }

                if (screen.State.CurrentValue.IsOneOf(ScreenState.Hidden, ScreenState.Unknown))
                {
                    Exception appearingException = null;
                    
                    yield return _holder.StartThrowingTimeLimitedCoroutine(screen.SetActiveAsync(true), exception => appearingException = exception);
                    
                    if (appearingException != null)
                    {
                        HandleExceptionAndRemoveScreenEverywhere(screenData, appearingException);
                    }
                }
            }
        }

        private void HandleExceptionAndRemoveScreenEverywhere(ScreenData screenData, Exception exception)
        {
            EventStreams.UserInterface.Publish(new ScreenSetActiveException(screenData, exception));
            Debug.LogError($"Have caught an exception on SetActiveAsync of screen {screenData.Id}\n{exception}");
        }

        public void RemoveFromRequestAndUnload(ScreenData screen)
        {
            foreach (var appearingScreensRequest in Requests)
            {
                RemoveScreenMentions(appearingScreensRequest.ScreensToHide, screen);
                RemoveScreenMentions(appearingScreensRequest.ScreensToShow, screen);
                RemoveScreenMentions(appearingScreensRequest.ScreensToUnload, screen);
            }
            
            var screensToUnload = _listsPool.Get();
            screensToUnload.Add(screen);
            UnloadAndReleaseProvidedList(screensToUnload);
        }

        private static void RemoveScreenMentions(List<ScreenData> screens, ScreenData screen)
        {
            for (var i = screens.Count - 1; i >= 0; i--)
            {
                if (screens[i].ScreenGuid == screen.ScreenGuid)
                {
                    screens.RemoveAt(i);
                }
            }
        }

        private IEnumerator HideScreens(List<ScreenData> screensToHide)
        {
            var screensCopy = new List<ScreenData>(screensToHide);
            foreach (var screenData in screensCopy)
            {
                if (!_screensCache.IsLoaded(screenData.Id))
                {
                    Debug.LogError($"Bug: Cannot hide not loaded screen ({screenData.Id}). It was skipped.");
                    continue;
                }
                
                var screen = _screensCache.GetScreen(screenData.Id);
                Assert.IsTrue(screen.State.CurrentValue.IsOneOf(ScreenState.Shown, ScreenState.Hidden, ScreenState.Unknown));

                if (screen.State.CurrentValue == ScreenState.Shown)
                {
                    Exception disappearingException = null;
                    
                    yield return _holder.StartThrowingTimeLimitedCoroutine(screen.SetActiveAsync(false), exception => disappearingException = exception);
                    
                    if (disappearingException != null)
                    {
                        HandleExceptionAndRemoveScreenEverywhere(screenData, disappearingException);
                    }
                }

                ScreenHiddenEvent.Create(screenData).Publish(EventStreams.UserInterface);
            }
        }
        
        public void Reset(IEnumerable<ScreenData> openedScreens, bool withoutAnimations)
        {
            var openedScreensList = _listsPool.Get();
            openedScreensList.AddRange(openedScreens);

            if (withoutAnimations)
            {
                ResetInstantly(openedScreensList);
            }
            else
            {
                AddToQueueAndTryProcess(_listsPool.Get(), _listsPool.Get(), openedScreensList);
            }
        }

        private void ResetInstantly(List<ScreenData> openedScreensList)
        {
            if (_showScreensCoroutine != null && _isInProgress)
            {
                _holder.StopCoroutine(_showScreensCoroutine);
                Requests.Enqueue(_currentRequest);
            }

            var plannedUnloadingScreens = _listsPool.Get();
            foreach (var appearingScreensRequest in Requests)
            {
                plannedUnloadingScreens.AddRange(appearingScreensRequest.ScreensToUnload);

                appearingScreensRequest.ReleaseAllLists(_listsPool);
            }

            // important to clear requests before unloading, because it can have a influence on unloading process
            Requests.Clear();

            UnloadAndReleaseProvidedList(plannedUnloadingScreens);
            UnloadAndReleaseProvidedList(openedScreensList);

            _isInProgress = false;

            _listsPool.Release(plannedUnloadingScreens);
        }
    }
}
