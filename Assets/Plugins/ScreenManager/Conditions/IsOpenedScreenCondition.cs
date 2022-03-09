using System.Linq;
using ScreenManager.Enums;
using ScreenManager.Events;

namespace ScreenManager.Conditions
{
    public class IsOpenedScreenCondition : IScreenCondition
    {
        private readonly ScreenId _screenId;

        public IsOpenedScreenCondition(ScreenId screenId)
        {
            _screenId = screenId;
        }
    
        public bool IsSatisfied()
        {
            var getOpenedScreensEvent = new GetOpenedScreensEvent();
            EventStreams.UserInterface.Publish(getOpenedScreensEvent);
            return getOpenedScreensEvent.OpenedScreens.Any(screen => screen.Id == _screenId);
        }

        public void Dispose()
        {
        }
    }
}