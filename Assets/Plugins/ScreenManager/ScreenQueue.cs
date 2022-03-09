using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ScreenManager.Enums;
using ScreenManager.Events;
using SimpleBus.Extensions;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.Assertions;

namespace ScreenManager
{
    public class ScreenQueue : IDisposable
    {
        private readonly List<QueueScreenData> _screensQueue = new List<QueueScreenData>();
        private readonly ScreenId _parentId;
        private readonly IDisposable _subscription;

        public ScreenQueue(ScreenId parentId)
        {
            _parentId = parentId;
            _subscription = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<GetScreenDebugDataEvent>(GetScreenDebugDataHandler),
                EventStreams.UserInterface.Subscribe<ScreenUnloadedEvent>(ScreenClosedHandler),
            };
        }

        private void ScreenClosedHandler(ScreenUnloadedEvent screenUnloaded)
        {
            if (screenUnloaded.ScreenData.Id == _parentId)
            {
                foreach (var queueScreenData in _screensQueue)
                {
                    queueScreenData.Dispose();
                }

                _screensQueue.Clear();
            }
        }

        private void GetScreenDebugDataHandler(GetScreenDebugDataEvent eventData)
        {
            if (eventData.ScreenQueue.ContainsKey(_parentId))
            {
                Debug.LogError($"Already has queue with type {_parentId}");
            }
            
            eventData.ScreenQueue[_parentId] = _screensQueue;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            foreach (var queueScreenData in _screensQueue)
            {
                queueScreenData?.Dispose();
            }
        }
        
        public void Remove(ScreenId id)
        {
            for (var i = _screensQueue.Count - 1; i >= 0; i--)
            {
                if (_screensQueue[i].ScreenData.Id == id)
                {
                    _screensQueue.RemoveAt(i);
                }
            }
        }

        public void Remove(Guid guid)
        {
            for (var i = _screensQueue.Count - 1; i >= 0; i--)
            {
                if (_screensQueue[i].ScreenData.ScreenGuid == guid)
                {
                    _screensQueue.RemoveAt(i);
                }
            }
        }

        private void TryShowNext()
        {
            var screenToShow = GetFirstReadyToShowScreen();
            if (screenToShow != null)
            {
                ShowNextScreen(screenToShow);
            }
        }

        public bool HasScreen(ScreenId screen)
        {
            return _screensQueue.Any(queueScreen => queueScreen.ScreenData.Id == screen);
        }

        public void Enqueue(QueueScreenData screen)
        {
            Debug.Log($"[Queue] Added new screen : {screen.ScreenData.Id}");
            _screensQueue.Add(screen);
            _screensQueue.Sort((screen1, screen2) => screen2.Priority.CompareTo(screen1.Priority));
        }

        private void RemoveExpiredScreensFromQueue()
        {
            for (var i = _screensQueue.Count - 1; i >= 0; i--)
            {
                var queueScreen = _screensQueue[i];

                if (queueScreen.IsExpired())
                {
                    queueScreen.Dispose();
                    Debug.Log($"[Queue] Screen expired : {queueScreen.ScreenData.Id}");

                    _screensQueue.RemoveAt(i);
                }
            }
        }

        [CanBeNull]
        private QueueScreenData GetFirstReadyToShowScreen()
        {
            RemoveExpiredScreensFromQueue();

            foreach (var screen in _screensQueue)
            {
                if (screen.IsReadyToShow())
                {
                    Assert.IsFalse(screen.IsExpired());

                    return screen;
                }

                if (screen.IsBlockingQueue)
                {
                    break;
                }
            }

            return null;
        }

        private void ShowNextScreen(QueueScreenData screen)
        {
            Assert.IsTrue(_screensQueue.Count > 0);
            
            Debug.Log($"[Queue] Show Next From Queue : {screen.ScreenData.Id}");

            _screensQueue.Remove(screen);
            screen.Dispose();

            OpenScreenEvent.Create(screen.ScreenData).Publish(EventStreams.UserInterface);
        }

        public void Update()
        {
            if (_screensQueue.Count > 0)
            {
                TryShowNext();
            }
        }
    }
}