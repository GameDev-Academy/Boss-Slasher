namespace ScreenManager.Conditions
{
    public class InvertedScreenCondition : IScreenCondition
    {
        private readonly IScreenCondition _condition;

        public InvertedScreenCondition(IScreenCondition condition)
        {
            _condition = condition;
        }

        public bool IsSatisfied()
        {
            return !_condition.IsSatisfied();
        }

        public void Dispose()
        {
            _condition?.Dispose();
        }
    }
}