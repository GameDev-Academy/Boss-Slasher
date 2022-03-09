using System;
using MicroRx.Core;
using ScreenManager.Enums;

namespace ScreenManager.Interfaces
{
    public interface IScreen : IAppearingScreenBehaviour
    {
        IObservableSubject<ScreenState> State { get; }
        ScreenId Id { get; }
        Guid Guid { get; set; }

        void Initialize(object context);
        void SetDrawingOrder(int order);
    }
}
