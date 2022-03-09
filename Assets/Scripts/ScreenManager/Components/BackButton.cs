using JetBrains.Annotations;
using ScreenManager.Events;
using SimpleBus.Extensions;
using UnityEngine;

namespace ScreenManager.Loaders.Scenes
{
    public class BackButton : MonoBehaviour
    {
        [UsedImplicitly]
        public void Back()
        {
            ScreensManager.Back();
        }
    }
}
