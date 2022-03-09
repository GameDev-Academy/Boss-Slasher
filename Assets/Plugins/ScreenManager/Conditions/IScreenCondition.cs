using System;

namespace ScreenManager.Conditions
{
    public interface IScreenCondition : IDisposable
    {
        bool IsSatisfied();
    }
}