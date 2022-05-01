using System;
using Unity.VisualScripting;
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

        public bool IsFight
        {
            get => _animator.GetBool(_fight);
            set => _animator.SetBool(_fight, value);
        }
        public bool IsRun
        {
            get => _animator.GetBool(_moving);
            set => _animator.SetBool(_moving, value);
        }
        
        private static readonly int _fight = Animator.StringToHash("isFight");
        private static readonly int _moving = Animator.StringToHash("isRun");
        
        private void Start()
        {
            IsRun = true;
        }
        
        public void Run()
        {
            IsFight = false;
        }
        
        public void Hit()
        {
            IsFight = true;
        }

        public void Die()
        {
             
        }
    }
}