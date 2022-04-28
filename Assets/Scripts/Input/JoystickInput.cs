using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickInput : IInputService, IDisposable
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    
    private PlayerInputActions _inputAction;

    public void Init()
    {
        Debug.Log("input initialize");
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
        Debug.Log(MoveInput);
    }
}
