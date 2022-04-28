using UniRx;
using UnityEngine;

/// <summary>
/// Класс отвечает за движение(перемещение и вращение) игрока
/// </summary>
public class MovementSystem : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _rotationPerFrameFactor = 12f;
    
    private IInputService _input;
    private float _speed;


    private void FixedUpdate()
    {
        if (_input == null) return;
        var inputDirection = _input.MoveInput;
        var moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        
        HandleMovement(moveDirection, _speed);
        HandleRotation(moveDirection);
    }
    
    public void Initialize(IInputService inputService, ReactiveProperty<int> speed)
    {
        _input = inputService;
        _speed = speed.Value;
    }

    private void HandleMovement(Vector3 moveDirection , float speed)
    {
        if (!(moveDirection.sqrMagnitude > Mathf.Epsilon)) return;
        _characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    private void HandleRotation(Vector3 direction)
    {
        var positionToLookAt = direction;
        positionToLookAt.y = 0f;
        Quaternion currentRotation = transform.rotation;
        
        if (positionToLookAt.sqrMagnitude < Mathf.Epsilon) return;

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationPerFrameFactor * Time.deltaTime);
    }
}
