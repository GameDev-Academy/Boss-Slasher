using ScreenManager.Enums;

public static class ScreenType
{
    public static ScreenId Get<TScreen>()
    {
        var typeName = typeof(TScreen).Name;
        return typeName.GetHashCode();
    }
}
