using System.Collections.Generic;
using ScreenManager.Enums;
using ScreenManager.Interfaces;
using SimpleEventBus.Events;

namespace ScreenManager.Events
{
    public class GetScreenDebugDataEvent : EventBase
    {
        public Dictionary<ScreenId, List<QueueScreenData>> ScreenQueue = new Dictionary<ScreenId, List<QueueScreenData>>();
        public Stack<ScreenData> ScreenStack;
        public bool IsAppearingProcessorWorking;
        public ScreenAppearingRequestDebugInformation LastRequestInformation;
        public Queue<AppearingScreensRequest> AppearingScreensRequests;
        public Dictionary<ScreenId, IScreen> ScreensCache;
    }
}