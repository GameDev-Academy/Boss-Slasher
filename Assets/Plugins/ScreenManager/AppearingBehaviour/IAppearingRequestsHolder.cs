using System.Collections.Generic;

namespace ScreenManager
{
    public interface IAppearingRequestsHolder
    {
        Queue<AppearingScreensRequest> Requests { get; }
    }
}