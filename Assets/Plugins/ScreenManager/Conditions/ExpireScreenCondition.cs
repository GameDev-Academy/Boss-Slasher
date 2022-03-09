using System;

namespace ScreenManager.Conditions
{
    public class ExpireScreenCondition : IScreenCondition
    {
        private readonly DateTime _expireTime;

        public ExpireScreenCondition(DateTime expireTime)
        {
            _expireTime = expireTime;
        }

        public bool IsSatisfied()
        {
            return DateTime.UtcNow > _expireTime;
        }

        public void Dispose()
        {
        }
    }
}
