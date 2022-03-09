using ScreenManager.Extensions;
using UnityEngine.Assertions;

namespace ScreenManager.Core
{
    public abstract class UIScreen<TContext> : UIScreen, IScreenWithTypedContext<TContext>
        where TContext : class
    {
        public override void Initialize(object context)
        {
            base.Initialize(context);
            var typedContext = ScreenContext.Get<TContext>(context);
            Assert.IsNotNull(typedContext, "Context is null!");
            Initialize(typedContext);
        }

        public abstract void Initialize(TContext context);
    }
}
