using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService, IDisposable
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    
    private readonly PlayerInputActions _inputAction;

    public InputService()
    {
        _inputAction = new PlayerInputActions();
        _inputAction.Player.Enable();

        _inputAction.Player.Move.performed += SetMoveInput;
        _inputAction.Player.Move.canceled += SetMoveInput;
    }

    public void Dispose()
    {
        _inputAction.Player.Move.performed -= SetMoveInput;
        _inputAction.Player.Move.canceled -= SetMoveInput;           
        _inputAction.Player.Disable();
    }

    private void SetMoveInput(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }
}
