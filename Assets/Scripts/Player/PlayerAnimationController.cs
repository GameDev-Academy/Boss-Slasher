using UnityEngine;

/// <summary>
/// Класс, отвечающий за анимации игрока
/// </summary>
public class PlayerAnimationController : MonoBehaviour, IAnimatorController
{
    [SerializeField] private Animator _animator;

    public int NumAttack
    {
        get => _animator.GetInteger(_numAttack);
        set => _animator.SetInteger(_numAttack, value);
    }
    
    private readonly int _moveHash = Animator.StringToHash("isRun");
    private static readonly int _numAttack = Animator.StringToHash("NumAttack");
    private static readonly int _fight = Animator.StringToHash("Fight");
    private int _counter = 0;
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
    
    public void Hit()
    {
        _counter = Random.Range(1, 4);
        switch (_counter)
        {
            case 1:
                NumAttack = 1;
                break;
            case 2:
                NumAttack = 2;
                break;
            case 3:
                NumAttack = 3;
                break;
        }
            
        _animator.SetTrigger(_fight);
    }

    public void Die()
    {
             
    }
}