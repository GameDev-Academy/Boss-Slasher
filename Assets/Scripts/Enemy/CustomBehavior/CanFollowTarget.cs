using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.CustomBehavior
{
    public class CanFollowTarget : Conditional
    {
        public TriggerObserver Observer;
        private bool _isAggro;


        public override void OnAwake()
        {
            base.OnAwake();
            
            Observer.TriggerEnter += TriggerEnter;
            Observer.TriggerExit += TriggerExit;
        }

        public override void OnReset()
        {
            base.OnReset();
            
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

            return TaskStatus.Running;
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