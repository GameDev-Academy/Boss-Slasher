using UnityEngine;

namespace Animators
{
    /// <summary>
    /// Меняет анимации по запросу
    /// </summary>
    public class PlayerAnimatorController : MonoBehaviour, IAnimatorController
    {
        [SerializeField]
        private Animator _animator;

        private int _counter = 0;

        public int NumAttack
        {
            get => _animator.GetInteger(_numAttack);
            set => _animator.SetInteger(_numAttack, value);
        }
        
        private static readonly int _numAttack = Animator.StringToHash("NumAttack");
        
        private static readonly int _fight = Animator.StringToHash("Fight");

        public void Hit()
        {
            _counter++;
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
                    _counter = 0;
                    break;
            }
            
            _animator.SetTrigger(_fight);
        }

        public void Die()
        {
             
        }
    }
}