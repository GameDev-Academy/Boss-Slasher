using System;
using ScreenManager;
using ScreenManager.Conditions;
using ScreenManager.Enums;
using ScreenManager.Events;
using ScreenManager.Interfaces;
using ScreenManager.Loaders.Scenes;
using SimpleBus.Extensions;

public class ScreensManager : BaseScreenManager
{
    private SceneScreenSettingsProvider _settingsProvider;
    private ScreenManagerDebugMenuDrawer _screenManagerDebugMenuDrawer;

    protected override IScreenLoader GetScreenLoader() => new SceneAsScreenLoader(GetScreenSettingsProvider());

    protected override IScreenSettingsProvider GetScreenSettingsProvider()
    {
        return _settingsProvider;
    }

    protected override void Awake()
    {
        _settingsProvider = new SceneScreenSettingsProvider();
        _screenManagerDebugMenuDrawer = new ScreenManagerDebugMenuDrawer();

        base.Awake();
    }

    protected override void OnDestroy()
    {
        _settingsProvider?.Dispose();
        _screenManagerDebugMenuDrawer?.Dispose();

        base.OnDestroy();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _subscriptions.Add(EventStreams.UserInterface.Subscribe<ScreenSetActiveException>(HandleScreenSetActiveException));
    }

    private void HandleScreenSetActiveException(ScreenSetActiveException exception)
    {
    }

    #region ScreenManager public API

    /// <summary>
    /// </summary>
    /// <param name="queueType"></param>
    /// <param name="context"></param>
    /// <param name="showCondition"></param>
    /// <param name="expireCondition"></param>
    /// <param name="shouldHidePrevious"></param>
    /// <param name="isBlockingQueue"></param>
    /// <param name="showLoadingIndicator"></param>
    /// <param name="priority">Screen with the highest priority shows first</param>
    /// <typeparam name="TScreen"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public static void EnqueueScreen<TScreen, TContext>(ScreenId queueType, TContext context,
        IScreenCondition showCondition = null, IScreenCondition expireCondition = null, bool shouldHidePrevious = false,
        bool isBlockingQueue = true, bool showLoadingIndicator = false, int priority = 0)
        where TScreen : IScreenWithTypedContext<TContext>
    {
        var screenTypeName = typeof(TScreen).Name;
        var enqueueScreenEvent = EnqueueScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, isBlockingQueue, showLoadingIndicator);
        enqueueScreenEvent.AddScreenContext(context);
        enqueueScreenEvent.AddShowConditions(showCondition);
        enqueueScreenEvent.AddExpireConditions(expireCondition);
        enqueueScreenEvent.SetQueueParentType(queueType);
        enqueueScreenEvent.Screen.Priority = priority;
        enqueueScreenEvent.Publish(EventStreams.UserInterface);
    }

    /// <summary>
    /// </summary>
    /// <param name="queueType"></param>
    /// <param name="context"></param>
    /// <param name="showCondition"></param>
    /// <param name="expireCondition"></param>
    /// <param name="shouldHidePrevious"></param>
    /// <param name="isBlockingQueue"></param>
    /// <param name="showLoadingIndicator"></param>
    /// <param name="priority">Screen with the highest priority shows first</param>
    /// <typeparam name="TScreen"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public static void EnqueueScreen<TScreen>(ScreenId queueType,
        IScreenCondition showCondition = null, IScreenCondition expireCondition = null, bool shouldHidePrevious = false,
        bool isBlockingQueue = true, bool showLoadingIndicator = false, int priority = 0)
    {
        var screenTypeName = typeof(TScreen).Name;
        var enqueueScreenEvent = EnqueueScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, isBlockingQueue, showLoadingIndicator);
        enqueueScreenEvent.AddShowConditions(showCondition);
        enqueueScreenEvent.AddExpireConditions(expireCondition);
        enqueueScreenEvent.SetQueueParentType(queueType);
        enqueueScreenEvent.Screen.Priority = priority;
        enqueueScreenEvent.Publish(EventStreams.UserInterface);
    }

    public static void EnqueueScreen<TScreen>(IScreenCondition showCondition = null, IScreenCondition expireCondition = null,
        bool shouldHidePrevious = false,
        bool isBlockingQueue = true, bool showLoadingIndicator = false, int priority = 0)
    {
        var screenTypeName = typeof(TScreen).Name;
        var enqueueScreenEvent = EnqueueScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, isBlockingQueue, showLoadingIndicator);
        enqueueScreenEvent.AddShowConditions(showCondition);
        enqueueScreenEvent.AddExpireConditions(expireCondition);
        enqueueScreenEvent.Screen.Priority = priority;
        enqueueScreenEvent.Publish(EventStreams.UserInterface);
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="showCondition"></param>
    /// <param name="expireCondition"></param>
    /// <param name="shouldHidePrevious"></param>
    /// <param name="isBlockingQueue"></param>
    /// <param name="showLoadingIndicator"></param>
    /// <param name="priority">Screen with the highest priority shows first</param>
    /// <typeparam name="TScreen"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public static void EnqueueScreen<TScreen, TContext>(TContext context, IScreenCondition showCondition = null,
        IScreenCondition expireCondition = null, bool shouldHidePrevious = false, bool isBlockingQueue = true,
        bool showLoadingIndicator = false, int priority = 0)
        where TScreen : IScreenWithTypedContext<TContext>
    {
        var screenTypeName = typeof(TScreen).Name;
        var enqueueScreenEvent = EnqueueScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, isBlockingQueue, showLoadingIndicator);
        enqueueScreenEvent.AddScreenContext(context);
        enqueueScreenEvent.AddShowConditions(showCondition);
        enqueueScreenEvent.AddExpireConditions(expireCondition);
        enqueueScreenEvent.Screen.Priority = priority;
        enqueueScreenEvent.Publish(EventStreams.UserInterface);
    }

    public static void EnqueueScreen<TScreen, TContext>(TContext context, Func<bool> showCondition, Func<bool> expireCondition = null,
        bool shouldHidePrevious = false, bool isBlockingQueue = true, int priority = 0)
        where TScreen : IScreenWithTypedContext<TContext>
    {
        var screenTypeName = typeof(TScreen).Name;
        var enqueueScreenEvent = EnqueueScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, isBlockingQueue);
        enqueueScreenEvent.AddScreenContext(context);
        enqueueScreenEvent.AddShowConditions(new DelegateScreenCondition(showCondition));
        enqueueScreenEvent.AddExpireConditions(new DelegateScreenCondition(expireCondition));
        enqueueScreenEvent.Screen.Priority = priority;
        enqueueScreenEvent.Publish(EventStreams.UserInterface);
    }

    public static void OpenScreen<TScreen, TContext>(TContext context, bool shouldHidePrevious = false, bool showLoadingIndicator = true)
        where TScreen : IScreenWithTypedContext<TContext>
    {
        var screenTypeName = typeof(TScreen).Name;
        OpenScreenEvent.Create(screenTypeName.GetHashCode(), shouldHidePrevious, showLoadingIndicator)
            .AddScreenContext(context)
            .Publish(EventStreams.UserInterface);
    }

    public static void CloseScreen<TScreen>(bool removeFromQueue = false)
        where TScreen : IScreen
    {
        var screenTypeName = typeof(TScreen).Name;
        CloseScreensByTypeEvent.Create(screenTypeName.GetHashCode(), removeFromQueue)
            .Publish(EventStreams.UserInterface);
    }

    public static bool IsOpenedOrPlanned<TScreen>()
        where TScreen : IScreen
    {
        var screenTypeName = typeof(TScreen).Name;
        return IsOpenedOrPlannedEvent.Check(screenTypeName.GetHashCode());
    }

    public static bool IsPlanned<TScreen>()
        where TScreen : IScreen
    {
        var screenTypeName = typeof(TScreen).Name;
        return IsPlannedEvent.Check(screenTypeName.GetHashCode());
    }

    public static bool IsOpened<TScreen>()
        where TScreen : IScreen
    {
        var screenTypeName = typeof(TScreen).Name;
        return IsScreenOpenedEvent.Check(screenTypeName.GetHashCode());
    }

    public static void Back<TScreen>()
        where TScreen : IScreen
    {
        var screenTypeName = typeof(TScreen).Name;
        BackToPreviousScreenEvent.Create(screenTypeName.GetHashCode())
            .Publish(EventStreams.UserInterface);
    }

    public static void Back()
    {
        BackToPreviousScreenEvent.Create().Publish(EventStreams.UserInterface);
    }

    public static ScreenId? GetTopScreen()
    {
        var getTopScreenEvent = new GetTopScreenEvent();
        getTopScreenEvent.Publish(EventStreams.UserInterface);
        return getTopScreenEvent.TopScreen;
    }

    public static void CloseAllScreens()
    {
        var closeAllScreens = new CloseAllScreensEvent();
        closeAllScreens.Publish(EventStreams.UserInterface);
    }

    #endregion
}