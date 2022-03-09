using System.Collections.Generic;
using JetBrains.Annotations;

namespace ScreenManager
{
    public struct AppearingScreensRequest
    {
        public AppearingScreensRequest(
            [NotNull] List<ScreenData> show, 
            [NotNull] List<ScreenData> hide, 
            [NotNull] List<ScreenData> unload)
        {
            ScreensToHide = hide;
            ScreensToShow = show;
            ScreensToUnload = unload;
        }

        public void ReleaseAllLists(ObjectPool<List<ScreenData>> pool)
        {
            pool.Release(ScreensToHide);
            pool.Release(ScreensToShow);
            pool.Release(ScreensToUnload);
        }

        [NotNull]
        public List<ScreenData> ScreensToShow;
        [NotNull]
        public List<ScreenData> ScreensToHide;
        [NotNull]
        public List<ScreenData> ScreensToUnload;
    }
}
