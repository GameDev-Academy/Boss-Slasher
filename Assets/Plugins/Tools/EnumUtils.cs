using System;

namespace ScreenManager
{
    public static class EnumUtils
    {
        public static bool IsOneOf<T>(this T value, params T[] otherValues) where T : Enum
        {
            foreach (var enumValue in otherValues)
            {
                if (value.Equals(enumValue))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
