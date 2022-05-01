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
    private bool _isInitialized;
    
    
    public void Initialize(IInputService inputService, ReactiveProperty<int> speed)
    {
        _isInitialized = true;
        _input = inputService;
        _speed = speed.Value;
    }
    
    private void FixedUpdate()
    {
        if (!_isInitialized)
        {
            return;
        }
        
        var inputDirection = _input.MoveInput;
        var moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        
        HandleMovement(moveDirection, _speed);
        HandleRotation(moveDirection);
    }
    
    private void HandleMovement(Vector3 moveDirection , float speed)
    {
        if (moveDirection.sqrMagnitude < Mathf.Epsilon)
        {
            return;
        }
        
        _characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    private void HandleRotation(Vector3 lookAtDirection)
    {
        if (lookAtDirection.sqrMagnitude < Mathf.Epsilon)
        {
            return;
        }

        var targetRotation = Quaternion.LookRotation(lookAtDirection);
        var currentRotation = transform.rotation;
        
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationPerFrameFactor * Time.deltaTime);
    }
}
