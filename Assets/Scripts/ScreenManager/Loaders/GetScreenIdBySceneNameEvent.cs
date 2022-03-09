using SimpleBus.Extensions;
using SimpleEventBus.Events;

public class GetScreenIdBySceneNameEvent : EventBase
{
    public int? Id;
    public string Scene;

    public static int? Invoke(string scene)
    {
        var eventData = EventFactory.Create<GetScreenIdBySceneNameEvent>();
        eventData.Id = null;
        eventData.Scene = scene;
        eventData.Publish(EventStreams.UserInterface);

        return eventData.Id;
    }
}
