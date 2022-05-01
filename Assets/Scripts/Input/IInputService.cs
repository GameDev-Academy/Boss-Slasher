using UnityEngine;

public interface IInputService : IService
{
    public Vector2 MoveInput { get; }
}
