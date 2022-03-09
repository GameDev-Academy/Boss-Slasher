using System.Collections;
using MicroRx.Core;
using ScreenManager.Interfaces;

namespace ScreenManager
{
    public class EmptyAppearingBehaviour : IAppearingScreenBehaviour
    {
        public IEnumerator SetActiveAsync(bool state)
        {
            yield break;
        }
    }
}
