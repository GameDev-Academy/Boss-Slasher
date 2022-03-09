using ScreenManager.Interfaces;

public interface IScreenWithTypedContext<TContext> : IScreen
{
    void Initialize(TContext context);
}
