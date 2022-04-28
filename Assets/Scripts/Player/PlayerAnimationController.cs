using UnityEngine;

/// <summary>
/// Класс, отвечающий за анимации игрока
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private readonly int _moveHash = Animator.StringToHash("isRun");
    
    private float _moveDampTime = 0.1f;
    private IInputService _inputService;


    public void Initialize(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (_inputService == null) return;
        var moveInput = _inputService.MoveInput;
        
        var isRunning = moveInput.sqrMagnitude > Mathf.Epsilon;
        
        _animator.SetBool(_moveHash, isRunning);
    }
}
