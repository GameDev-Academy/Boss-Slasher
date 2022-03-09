using System;
using SimpleBus.Extensions;
using SimpleEventBus.Events;

public class GetBundleUrlEvent : EventBase
{
    public string Scene;
    public string Url;

    public static string Invoke(string scene)
    {
        var eventData = EventFactory.Create<GetBundleUrlEvent>();
        eventData.Url = String.Empty;
        eventData.Scene = scene;
        eventData.Publish(EventStreams.UserInterface);

        return eventData.Url;
    }
}
