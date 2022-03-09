namespace ScreenManager.Conditions
{
    public static class ScreenConditionUtils
    {
        public static IScreenCondition Invert(this IScreenCondition condition)
        {
            return new InvertedScreenCondition(condition);
        }
    }
}