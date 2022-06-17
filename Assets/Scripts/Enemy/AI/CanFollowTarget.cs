using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;

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
        private TriggerObserver _observer;
        private bool _isAggro;
        
        public override void OnAwake()
        {
            base.OnAwake();
            _observer.TriggerEnter += TriggerEnter;
            _observer.TriggerExit += TriggerExit;
        }

        public override void OnBehaviorComplete()
        {
            base.OnBehaviorComplete();
            _isAggro = false;
            _observer.TriggerEnter -= TriggerEnter;
            _observer.TriggerExit -= TriggerExit;
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