using System.Collections.Generic;
using ScreenManager.Enums;
using ScreenManager.Interfaces;

namespace ScreenManager
{
    public class ScreenUnloadingHelper
    {
        private readonly IScreenSettingsProvider _screenSettingsProvider;
        private readonly IScreenStackHolder _screenStackHolder;
        private readonly IAppearingRequestsHolder _appearingRequestsHolder;

        public ScreenUnloadingHelper(
            IScreenStackHolder screenStackHolder,
            IAppearingRequestsHolder appearingRequestsHolder,
            IScreenSettingsProvider screenSettingsProvider)
        {
            _appearingRequestsHolder = appearingRequestsHolder;
            _screenStackHolder = screenStackHolder;
            _screenSettingsProvider = screenSettingsProvider;
        }

        /// <summary>
        /// If there is any screen with same settings in the screens stack or in the queue
        /// we should prevent unloading it
        /// </summary>
        public bool IsAllowedToUnload(ScreenId id)
        {
            var currentScreenSettings = _screenSettingsProvider.Get(id);

            if (HasInStackWithSameSettings(currentScreenSettings))
            {
                return false;
            }

            foreach (var request in _appearingRequestsHolder.Requests)
            {
                if (HasAnyScreenWithSameSettings(request.ScreensToShow, currentScreenSettings))
                {
                    return false;
                }
                
                if (HasAnyScreenWithSameSettings(request.ScreensToHide, currentScreenSettings))
                {
                    return false;
                }
                
                if (HasAnyScreenWithSameSettings(request.ScreensToUnload, currentScreenSettings))
                {
                    return false;
                }
            }

            return true;
        }

        private bool HasAnyScreenWithSameSettings(IEnumerable<ScreenData> screens, IScreenSettings currentScreenSettings)
        {
            foreach (var openedScreen in screens)
            {
                var screenSettings = _screenSettingsProvider.Get(openedScreen.Id);
                if (screenSettings.Path == currentScreenSettings.Path)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasInStackWithSameSettings(IScreenSettings currentScreenSettings)
        {
            return HasAnyScreenWithSameSettings(_screenStackHolder.OpenedScreens, currentScreenSettings);
        }
    }
}