using System;
using ScreenManager.Enums;
using UnityEngine;

namespace ScreenManager.Interfaces
{
    public interface IScreenLoader : IDisposable
    {
        /// <summary>
        /// Throws ScreenLoadedEvent on finish loading
        /// </summary>
        void StartLoad(ScreenId id, bool showLoadingIndicator);
        AsyncOperation Unload(IScreen screen);
    }
}
