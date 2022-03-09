using System;
using System.Collections.Generic;
using System.Linq;
using ScreenManager.Enums;
using ScreenManager.Events;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace ScreenManager
{
    public class ScreenQueueManager : MonoBehaviour
    {
        private readonly Dictionary<ScreenId, ScreenQueue> _screenQueues = new Dictionary<ScreenId, ScreenQueue>();
        private IDisposable _subscriptions;
        private ScreenId _topScreen = ScreenId.None;

        private void OnEnable()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<CloseAllScreensEvent>(CloseAllScreensHandler),
                EventStreams.UserInterface.Subscribe<EnqueueScreenEvent>(EnqueueScreenHandler),
                EventStreams.UserInterface.Subscribe<CloseScreensByTypeEvent>(CloseScreensByTypeHandler),
                EventStreams.UserInterface.Subscribe<CloseScreenByGuidEvent>(CloseScreenByGuidHandler),
                EventStreams.UserInterface.Subscribe<TopScreenChangedEvent>(TopScreenChangedHandler),
                EventStreams.UserInterface.Subscribe<IsOpenedOrPlannedEvent>(IsOpenedOrPlannedHandler),
                EventStreams.UserInterface.Subscribe<IsPlannedEvent>(IsPlannedHandler),
            };
        }
        
        private void IsPlannedHandler(IsPlannedEvent eventData)
        {
            eventData.Result = _screenQueues.Values.Any(queue => queue.HasScreen(eventData.ScreenId));
        }

        private void IsOpenedOrPlannedHandler(IsOpenedOrPlannedEvent eventData)
        {
            if (eventData.Result)
            {
                return;
            }

            eventData.Result = _screenQueues.Values.Any(queue => queue.HasScreen(eventData.ScreenId));
        }

        private void OnDisable()
        {
            _subscriptions.Dispose();
        }

        private void LateUpdate()
        {
            if (_screenQueues.ContainsKey(_topScreen))
            {
                // we are updating the queue on LateUpdate because sometimes we want to close or open several screens
                // during one frame
                _screenQueues[_topScreen].Update();
            }
        }

        private void CloseAllScreensHandler(CloseAllScreensEvent eventData)
        {
            if (!eventData.CleanQueue)
            {
                return;
            }

            foreach (var queue in _screenQueues.Values)
            {
                queue?.Dispose();
            }

            _screenQueues.Clear();
        }
        
        private void CloseScreenByGuidHandler(CloseScreenByGuidEvent eventData)
        {
            if (eventData.RemoveFromQueue)
            {
                foreach (var screenQueue in _screenQueues.Values)
                {
                    screenQueue.Remove(eventData.Guid);
                }
            }
        }

        private void CloseScreensByTypeHandler(CloseScreensByTypeEvent closeScreenEvent)
        {
            if (closeScreenEvent.RemoveFromQueue)
            {
                foreach (var screenQueue in _screenQueues.Values)
                {
                    screenQueue.Remove(closeScreenEvent.ScreenId);
                }
            }
        }

        private void TopScreenChangedHandler(TopScreenChangedEvent eventData)
        {
            _topScreen = eventData.Id;
        }

        private void EnqueueScreenHandler(EnqueueScreenEvent openScreenEvent)
        {
            if (!_screenQueues.ContainsKey(openScreenEvent.QueueParentId))
            {
                _screenQueues[openScreenEvent.QueueParentId] = new ScreenQueue(openScreenEvent.QueueParentId);
            }

            _screenQueues[openScreenEvent.QueueParentId].Enqueue(openScreenEvent.Screen);
        }
    }
}
