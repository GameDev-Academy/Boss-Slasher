using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.AI
{
    /// <summary>
    /// Делает проверку вошел ли игрок в зону аггро
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public sealed class CanFollowTarget : Conditional
    {
        [SerializeField]
        private TriggerObserver Observer;
        private bool _isAggro;
        
        public override void OnAwake()
        {
            base.OnAwake();
            Observer.TriggerEnter += TriggerEnter;
            Observer.TriggerExit += TriggerExit;
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            _isAggro = false;
            Observer.TriggerEnter -= TriggerEnter;
            Observer.TriggerExit -= TriggerExit;
        }

        public override TaskStatus OnUpdate()
        {
            if (_isAggro)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private void TriggerEnter(Collider collider)
        {
            _isAggro = true;
        }

        private void TriggerExit(Collider collider)
        {
            _isAggro = false;
        }
    }
}