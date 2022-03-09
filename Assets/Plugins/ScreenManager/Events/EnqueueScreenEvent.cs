using System;
using System.Linq;
using ScreenManager.Conditions;
using ScreenManager.Enums;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class EnqueueScreenEvent : EventBase
    {
        public QueueScreenData Screen;
        public ScreenId QueueParentId;

        public static EnqueueScreenEvent Create(ScreenId screenId, 
            bool shouldHidePrevious = false, 
            bool isBlockingQueue = true, 
            bool showLoadingIndicator = false)
        {
            var eventData = EventFactory.Create<EnqueueScreenEvent>();
            var screenData = new ScreenData(screenId, null, shouldHidePrevious, showLoadingIndicator);
            eventData.Screen = new QueueScreenData(screenData, isBlockingQueue);
            eventData.QueueParentId = ScreenId.None;
            return eventData;
        }

        public EnqueueScreenEvent SetQueueParentType(ScreenId parentId)
        {
            QueueParentId = parentId;
            return this;
        }

        public EnqueueScreenEvent AddScreenContext(params object[] context)
        {
            var screenContext = context.Length == 1 ? context[0] : context;
            Screen.ScreenData.Context = screenContext;
            return this;
        }

        public EnqueueScreenEvent AddShowConditions(params IScreenCondition[] conditions)
        {
            var showConditions = GetConditions(conditions);
            Screen.ShowConditions = showConditions;
            return this;
        }

        public EnqueueScreenEvent AddExpireConditions(params IScreenCondition[] conditions)
        {
            var showConditions = GetConditions(conditions);
            Screen.ExpireConditions = showConditions;
            return this;
        }

        private static IScreenCondition GetConditions(IScreenCondition[] conditions)
        {
            if (conditions == null || conditions.All(condition => condition == null))
            {
                return null;
            }

            var showConditions = conditions.Length > 0 ? new CompositeScreenCondition(conditions) : conditions.First();
            return showConditions;
        }
    }
}
