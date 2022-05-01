using SimpleEventBus;
using SimpleEventBus.Interfaces;

public static class EventStreams
{
    public static IEventBus UserInterface { get; } = new EventBus();
    public static IEventBus Game { get; } = new EventBus();
}

