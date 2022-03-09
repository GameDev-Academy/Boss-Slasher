using System;
using ScreenManager.Enums;

namespace ScreenManager.Conditions
{
    public static class ScreenConditionsFactory
    {
        public static IScreenCondition IsScreenOpened(ScreenId screen)
        {
            return new IsOpenedScreenCondition(screen);
        }

        public static IScreenCondition IsExpired(DateTime expireTime)
        {
            return new ExpireScreenCondition(expireTime);
        }
    }
}
