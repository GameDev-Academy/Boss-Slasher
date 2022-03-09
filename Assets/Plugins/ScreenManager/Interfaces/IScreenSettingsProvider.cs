using JetBrains.Annotations;
using ScreenManager.Enums;

namespace ScreenManager.Interfaces
{
    public interface IScreenSettingsProvider
    {
        [NotNull]
        IScreenSettings Get(ScreenId id);
    }
}
