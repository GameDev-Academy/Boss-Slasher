using System;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class ScreenSetActiveException : EventBase
    {
        public ScreenSetActiveException(ScreenData screenData, Exception exception)
        {
            ScreenData = screenData;
            Exception = exception;
        }

        public ScreenData ScreenData;
        public Exception Exception;
    }
}
