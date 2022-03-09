namespace ScreenManager
{
    public class ScreenAppearingRequestDebugInformation
    {
        public AppearingScreensRequest Request;
        public bool UnloadScreensProcessed;
        public bool HideScreensProcessed;
        public bool ShowScreensProcessed;

        public void SetNewRequest(AppearingScreensRequest request)
        {
            Request = request;
            UnloadScreensProcessed = false;
            HideScreensProcessed = false;
            ShowScreensProcessed = false;
        }

        public void MarkUnloadScreensProcessed()
        {
            UnloadScreensProcessed = true;
        }

        public void MarkHideScreensProcessed()
        {
            HideScreensProcessed = true;
        }

        public void MarkShowScreensProcessed()
        {
            ShowScreensProcessed = true;
        }
    }
}