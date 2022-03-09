using System;
using System.Collections.Generic;
using ScreenManager.Enums;
using SimpleBus.Extensions;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public static class ScreenManagerUtils
    {
        public static ScreenId GetFirstScreen(Predicate<ScreenData> match)
        {
            var openedScreens = GetOpenedScreensEvent.Invoke();
            var parentScreenIndex = openedScreens.FindIndex(match);
            var parentId = parentScreenIndex >= 0 ? openedScreens[parentScreenIndex].Id : ScreenId.None;
            return parentId;
        }
    }
    
    public class GetOpenedScreensEvent : EventBase
    {
        public List<ScreenData> OpenedScreens = new List<ScreenData>();

        /// <returns>
        /// Returns stack of the opened screens represented in the list.
        /// It means that the first screen from this list is on the top.
        /// </returns>
        public static List<ScreenData> Invoke()
        {
            var eventData = EventFactory.Create<GetOpenedScreensEvent>();
            eventData.OpenedScreens.Clear();
            eventData.Publish(EventStreams.UserInterface);
            return eventData.OpenedScreens;
        }
    }
}
