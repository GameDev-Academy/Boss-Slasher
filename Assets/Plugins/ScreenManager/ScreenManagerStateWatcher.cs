using MicroRx.Core;
using ScreenManager.Events;
using SimpleBus.Extensions;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.Assertions;

namespace ScreenManager
{
    public class ScreenManagerStateWatcher : MonoBehaviour
    {
        private readonly Subject<bool> _isInProgress = new Subject<bool>(false);
        private CompositeDisposable _subscriptions;
        private int _openedScreensCount;

        private void OnEnable()
        {
            var getOpenedScreensEvent = new GetOpenedScreensEvent();
            getOpenedScreensEvent.Publish(EventStreams.UserInterface);
            _openedScreensCount = getOpenedScreensEvent.OpenedScreens.Count;

            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<ScreenOpeningEvent>(OnScreenOpeningHandler),
                EventStreams.UserInterface.Subscribe<ScreenClosedEvent>(OnScreenClosedHandler),
                EventStreams.UserInterface.Subscribe<GetScreenManagerStateEvent>(GetScreenManagerStateHandler),
            };
        }

        private void OnDisable()
        {
            _subscriptions?.Dispose();
        }

        private void GetScreenManagerStateHandler(GetScreenManagerStateEvent getStateEvent)
        {
            getStateEvent.IsInProgress = _isInProgress;
        }

        private void OnScreenClosedHandler(ScreenClosedEvent obj)
        {
            _openedScreensCount--;

            _isInProgress.OnNext(_openedScreensCount > 0);

            Assert.IsTrue(_openedScreensCount >= 0, "Something goes wrong, check events ScreenOpenedEvent/ScreenClosedEvent");
        }

        private void OnScreenOpeningHandler(ScreenOpeningEvent eventData)
        {
            _openedScreensCount++;

            _isInProgress.OnNext(true);
        }
    }
}
