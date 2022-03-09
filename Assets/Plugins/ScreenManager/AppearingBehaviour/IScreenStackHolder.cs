using System.Collections.Generic;

namespace ScreenManager
{
    public interface IScreenStackHolder
    {
        Stack<ScreenData> OpenedScreens { get; }
    }
}