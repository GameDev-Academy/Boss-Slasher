using System;

namespace ScreenManager.Conditions
{
    public class DelegateScreenCondition : IScreenCondition
    {
        private readonly Func<bool> _isSatisfied;

        public DelegateScreenCondition(Func<bool> isSatisfied)
        {
            _isSatisfied = isSatisfied;
        }

        public void Dispose()
        {
        }

        public bool IsSatisfied() => _isSatisfied.Invoke();
    }
}
