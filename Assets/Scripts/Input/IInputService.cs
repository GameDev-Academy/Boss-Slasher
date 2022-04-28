using UnityEngine;

public interface IInputService
{
    public Vector2 MoveInput { get; }
    public void Init();
}
