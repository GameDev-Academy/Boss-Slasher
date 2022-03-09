using System;
using ScreenManager.Enums;
using SimpleBus.Extensions;
using UnityEngine;

namespace ScreenManager.Events
{
    public class WaitWhileScreenOpenedOrPlanned : CustomYieldInstruction, IDisposable
    {
        public override bool keepWaiting => _keepWaiting;

        private readonly IDisposable _subscription;
        private readonly ScreenId _screenId;
        private bool _keepWaiting;

        public WaitWhileScreenOpenedOrPlanned(ScreenId screenId)
        {
            _screenId = screenId;
            var getOpenedScreens = new GetOpenedScreensEvent();
            getOpenedScreens.Publish(EventStreams.UserInterface);

            _keepWaiting = IsOpenedOrPlannedEvent.Check(screenId);

            if (_keepWaiting)
            {
                _subscription = EventStreams.UserInterface.Subscribe<ScreenClosedEvent>(OnScreenClosed);
            }
        }

        ~WaitWhileScreenOpenedOrPlanned()
        {
            Dispose();
        }

        private void OnScreenClosed(ScreenClosedEvent screenClosed)
        {
            if (screenClosed.ScreenData.Id == _screenId)
            {
                _subscription?.Dispose();
                _keepWaiting = false;
            }
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}