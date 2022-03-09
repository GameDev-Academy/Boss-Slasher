using System;
using System.Collections.Generic;
using System.Linq;
using ScreenManager.Enums;
using ScreenManager.Events;
using ScreenManager.Interfaces;
using SimpleBus.Extensions;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace ScreenManager
{
    public abstract class BaseScreenManager : MonoBehaviour, IScreenManager
    {
        public Stack<ScreenData> OpenedScreens => _openedScreens;
        
        protected CompositeDisposable _subscriptions;
        private Stack<ScreenData> _openedScreens = new Stack<ScreenData>();
        private ScreensAppearingProcessor _appearingProcessor;
        private ScreensCache _screensCache;
        private readonly ObjectPool<List<ScreenData>> _listsPool = new ObjectPool<List<ScreenData>>(list => list.Clear());

        protected virtual void Awake()
        { 
            _screensCache = new ScreensCache(GetScreenLoader());
            _appearingProcessor = new ScreensAppearingProcessor(this, _screensCache, _listsPool);
            
            var unloadingHelper = new ScreenUnloadingHelper(this, _appearingProcessor, GetScreenSettingsProvider());
            _screensCache.SetUnloadingHelper(unloadingHelper);
        }

        protected virtual void OnDestroy()
        {
            _screensCache?.Dispose();
            _appearingProcessor?.Dispose();
        }
        
        protected virtual void OnEnable()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<FailScreenLoadingEvent>(FailScreenLoadingHandler),
                EventStreams.UserInterface.Subscribe<ScreenSetActiveException>(OnScreenAppearingExceptionHandler),
                EventStreams.UserInterface.Subscribe<OpenScreenEvent>(OpenScreenEventHandler),
                EventStreams.UserInterface.Subscribe<BackToPreviousScreenEvent>(BackEventHandler),
                EventStreams.UserInterface.Subscribe<GetOpenedScreensEvent>(OnGetOpenedScreensEventHandler),
                EventStreams.UserInterface.Subscribe<ScreenLoadedEvent>(ScreenLoadedHandler),
                EventStreams.UserInterface.Subscribe<CloseAllScreensEvent>(CloseAllScreensHandler),
                EventStreams.UserInterface.Subscribe<CloseScreensByTypeEvent>(CloseScreensByTypeHandler),
                EventStreams.UserInterface.Subscribe<CloseScreenByGuidEvent>(CloseScreenByGuidHandler),
                EventStreams.UserInterface.Subscribe<GetScreenDebugDataEvent>(GetScreenDebugDataHandler),
                EventStreams.UserInterface.Subscribe<GetScreenSettingsProviderEvent>(GetScreenSettingsProviderHandler),
                EventStreams.UserInterface.Subscribe<IsOpenedOrPlannedEvent>(IsOpenedOrPlannedHandler),
                EventStreams.UserInterface.Subscribe<IsScreenOpenedEvent>(IsScreenOpenedHandler),
                EventStreams.UserInterface.Subscribe<GetTopScreenEvent>(GetTopScreenHandler),
            };
        }

        protected abstract IScreenLoader GetScreenLoader();
        protected abstract IScreenSettingsProvider GetScreenSettingsProvider();

        private void OnScreenAppearingExceptionHandler(ScreenSetActiveException exceptionData)
        {
            RemoveScreenFromStackAndFromAppearingQueue(exceptionData.ScreenData);
        }

        private void RemoveScreenFromStackAndFromAppearingQueue(ScreenData screenData)
        {
            var newStack = _openedScreens.Where(screen => screen.ScreenGuid != screenData.ScreenGuid);
            var closedScreens = _openedScreens.Where(screen => screen.ScreenGuid == screenData.ScreenGuid);

            _openedScreens = new Stack<ScreenData>(newStack);

            try
            {
                foreach (var closingScreen in closedScreens)
                {
                    ScreenClosedEvent.Create(closingScreen).Publish(EventStreams.UserInterface);
                }

                OnUpdateTopScreen();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }

            _appearingProcessor.RemoveFromRequestAndUnload(screenData);
            _appearingProcessor.AddToQueueAndTryProcess(GetVisibleScreens(), _listsPool.Get(), _listsPool.Get());
        }

        private void OnUpdateTopScreen()
        {
            TopScreenChangedEvent.Create(_openedScreens.Count == 0 ? ScreenId.None : _openedScreens.Peek().Id)
                .Publish(EventStreams.UserInterface);
        }

        private void ScreenLoadedHandler(ScreenLoadedEvent screenLoadedEvent)
        {
            _screensCache.OnScreenLoaded(screenLoadedEvent);
            _appearingProcessor.TryProcessNext();
        }
        
        private void FailScreenLoadingHandler(FailScreenLoadingEvent eventData)
        {
            foreach (var openedScreen in _openedScreens.ToList())
            {
                if (openedScreen.Id == eventData.Type)
                {
                    RemoveScreenFromStackAndFromAppearingQueue(openedScreen);
                }
            }
        }

        private void IsScreenOpenedHandler(IsScreenOpenedEvent eventData)
        {
            eventData.Result = _openedScreens.Any(screen => screen.Id == eventData.ScreenId);
        }

        private void GetTopScreenHandler(GetTopScreenEvent eventData)
        {
            eventData.TopScreen = _openedScreens.Count > 0 ? (ScreenId?)_openedScreens.Peek().Id : null;
        }
        
        private void IsOpenedOrPlannedHandler(IsOpenedOrPlannedEvent eventData)
        {
            if (eventData.Result)
            {
                return;
            }

            eventData.Result = _openedScreens.Any(screen => screen.Id == eventData.ScreenId);
        }

        private void GetScreenSettingsProviderHandler(GetScreenSettingsProviderEvent getScreenSettingsProviderEvent)
        {
            getScreenSettingsProviderEvent.ScreenSettingsProvider = GetScreenSettingsProvider();
        }

        private void GetScreenDebugDataHandler(GetScreenDebugDataEvent eventData)
        {
            eventData.ScreenStack = _openedScreens;
        }

        private void CloseScreensByTypeHandler(CloseScreensByTypeEvent eventData)
        {
            CloseScreens(screenData => screenData.Id == eventData.ScreenId);
        }

        private void CloseScreenByGuidHandler(CloseScreenByGuidEvent eventData)
        {
            CloseScreens(screenData => screenData.ScreenGuid == eventData.Guid);
        }

        private void CloseScreens(Predicate<ScreenData> closePredicate)
        {
            var unloadingScreens = _listsPool.Get();
            var newStack = new Stack<ScreenData>();

            foreach (var openedScreen in _openedScreens.Reverse())
            {
                if (closePredicate?.Invoke(openedScreen) == true)
                {
                    unloadingScreens.Add(openedScreen);
                }
                else
                {
                    newStack.Push(openedScreen);
                }
            }

            _openedScreens = newStack;

            foreach (var unloadingScreen in unloadingScreens)
            {
                ScreenClosedEvent.Create(unloadingScreen).Publish(EventStreams.UserInterface);
            }

            var visibleScreens = GetVisibleScreens();
            _appearingProcessor.AddToQueueAndTryProcess(visibleScreens, _listsPool.Get(), unloadingScreens);

            OnUpdateTopScreen();
        }

        private void CloseAllScreensHandler(CloseAllScreensEvent eventData)
        {
            Log($"CloseAllScreensHandler. Instantly: {eventData.Instantly}. CleanQueue: {eventData.CleanQueue}");
            new ClosingAllScreensEvent().Publish(EventStreams.UserInterface);
            
            var copyOfStack = _openedScreens.ToList();
            _openedScreens.Clear();

            foreach (var openedScreen in copyOfStack)
            {
                ScreenClosedEvent.Create(openedScreen).Publish(EventStreams.UserInterface);
            }

            // it's important to clear the stack before calling this method
            _appearingProcessor.Reset(copyOfStack, eventData.Instantly);

            if (eventData.Instantly && eventData.CleanQueue)
            {
                _screensCache.Clear();
            }

            OnUpdateTopScreen();
        }

        private void OnGetOpenedScreensEventHandler(GetOpenedScreensEvent eventData)
        {
            eventData.OpenedScreens = eventData.OpenedScreens ?? new List<ScreenData>();
            // returns copy of stack to prevent any modifications
            eventData.OpenedScreens.AddRange(_openedScreens);
        }

        private void OnDisable()
        {
            _subscriptions.Dispose();
        }

        private void BackEventHandler(BackToPreviousScreenEvent eventData)
        {
            BackToPrevious(eventData.ScreenId);
        }

        private void OpenScreenEventHandler(OpenScreenEvent eventData)
        {
            OpenScreen(eventData.ScreenData);
        }

        public bool IsOpened(ScreenId screenId)
        {
            foreach (var openedScreen in _openedScreens)
            {
                if (openedScreen.Id == screenId)
                {
                    return true;
                }
            }

            return false;
        }

        public void BackToPrevious(ScreenId screenId)
        {
            var unloadingScreens = _listsPool.Get();

            ReturnToScreen(screenId, unloadingScreens);
            var visibleScreens = GetVisibleScreens();
            _appearingProcessor.AddToQueueAndTryProcess(visibleScreens, _listsPool.Get(), unloadingScreens);
            
            foreach (var unloadingScreen in unloadingScreens)
            {
                ScreenClosedEvent.Create(unloadingScreen).Publish(EventStreams.UserInterface);
            }

            OnUpdateTopScreen();

            Log($"Back to previous screen {(_openedScreens.Count > 0 ? _openedScreens.Peek().Id : ScreenId.None)}");
        }

        private void ReturnToScreen(ScreenId screenId, List<ScreenData> unloadingScreens)
        {
            var isSingleBack = screenId == ScreenId.None;
            while (_openedScreens.Count > 0 && _openedScreens.Peek().Id != screenId)
            {
                var screenData = _openedScreens.Pop();
                unloadingScreens.Add(screenData);

                if (isSingleBack)
                {
                    break;
                }
            }
        }

        private List<ScreenData> GetVisibleScreens()
        {
            var visibleScreens = _listsPool.Get();
            foreach (var screenData in _openedScreens)
            {
                visibleScreens.Add(screenData);

                if (screenData.ShouldHidePrevious)
                {
                    break;
                }
            }

            return visibleScreens;
        }

        public void OpenScreen(ScreenData screenData)
        {
            Log($"Open screen {screenData.Id}. (Screen was added to the stack.)");

            var lastStackPosition = _openedScreens.Count > 0 ? _openedScreens.Peek().StackPosition : 0;
            screenData.StackPosition = lastStackPosition + 1;

            var screensToHide = _listsPool.Get();
            if (screenData.ShouldHidePrevious)
            {
                screensToHide.AddRange(_openedScreens);
            }

            var screensToShow = _listsPool.Get();
            screensToShow.Add(screenData);

            _openedScreens.Push(screenData);
            _appearingProcessor.AddToQueueAndTryProcess(screensToShow, screensToHide, _listsPool.Get());

            OnUpdateTopScreen();

            ScreenOpeningEvent.Create(screenData).Publish(EventStreams.UserInterface);
            _screensCache.LoadScreen(screenData.Id, screenData.ShowLoadingIndicator);
        }

        private void Log(string message)
        {
            Debug.Log($"[{Time.frameCount}] {message}");
        }
    }
}
