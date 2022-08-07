using UnityEngine;

namespace Player
{
    /// <summary>
    /// Класс, отвечающий за анимации игрока
    /// </summary>
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _moveHash = Animator.StringToHash("isRun");
        private readonly int _dieHash = Animator.StringToHash("Dead");
        private readonly int _hitHash = Animator.StringToHash("Attack");

        private IInputService _inputService;


        public void Initialize(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            HandleAnimation();
        }

        public void PlayHit()
        {
            _animator.SetTrigger(_hitHash);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(_dieHash);
        }

        private void HandleAnimation()
        {
            if (_inputService == null) return;
            var moveInput = _inputService.MoveInput;
            var isRunning = moveInput.sqrMagnitude > Mathf.Epsilon;
            _animator.SetBool(_moveHash, isRunning);
        }
    }
}