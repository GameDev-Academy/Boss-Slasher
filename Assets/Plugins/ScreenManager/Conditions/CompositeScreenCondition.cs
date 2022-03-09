using System.Linq;

namespace ScreenManager.Conditions
{
    public class CompositeScreenCondition : IScreenCondition
    {
        private readonly IScreenCondition[] _conditions;

        public CompositeScreenCondition(params IScreenCondition[] conditions)
        {
            _conditions = conditions;
        }

        public bool IsSatisfied()
        {
            return _conditions.All(condition => condition.IsSatisfied());
        }

        public void Dispose()
        {
            foreach (var screenCondition in _conditions)
            {
                screenCondition?.Dispose();
            }
        }
    }
}
