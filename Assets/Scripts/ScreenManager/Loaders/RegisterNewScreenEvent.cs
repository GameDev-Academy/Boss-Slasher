using SimpleEventBus.Events;

public class RegisterNewScreenEvent : EventBase
{
    public RegisterNewScreenEvent(int id, string scene, string name)
    {
        Id = id;
        Name = name;
        Scene = scene;
    }

    public int Id;
    public string Name;
    public string Scene;
}
