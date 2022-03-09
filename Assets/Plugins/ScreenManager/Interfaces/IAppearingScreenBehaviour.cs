using System.Collections;
using MicroRx.Core;

namespace ScreenManager.Interfaces
{
    public interface IAppearingScreenBehaviour
    {
        IEnumerator SetActiveAsync(bool state);
    }
}
