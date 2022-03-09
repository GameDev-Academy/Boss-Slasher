using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class OpenScreenEvent : EventBase
    {
        public ScreenData ScreenData { get; private set; }

        public static OpenScreenEvent Create(ScreenData screenData)
        {
            var eventData = EventFactory.Create<OpenScreenEvent>();
            eventData.ScreenData = screenData;
            return eventData;
        }

        public static OpenScreenEvent Create(ScreenId screenId, 
            bool shouldHidePrevious = false,
            bool showLoadingIndicator = true,
            object context = null)
        {
            var eventData = EventFactory.Create<OpenScreenEvent>();
            eventData.ScreenData = new ScreenData(screenId, context, shouldHidePrevious, showLoadingIndicator);
            return eventData;
        }

        public OpenScreenEvent AddScreenContext(params object[] context)
        {
            var screenContext = context.Length == 1 ? context[0] : context;
            ScreenData = new ScreenData(ScreenData.Id, screenContext, ScreenData.ShouldHidePrevious);
            return this;
        }
    }
}