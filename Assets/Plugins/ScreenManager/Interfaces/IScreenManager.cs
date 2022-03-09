using System;
using ScreenManager.Enums;

namespace ScreenManager.Interfaces
{
    public interface IScreenManager : IScreenStackHolder
    {
        void OpenScreen(ScreenData screenData);
        void BackToPrevious(ScreenId screenId);
        bool IsOpened(ScreenId screenId);
    }
}
