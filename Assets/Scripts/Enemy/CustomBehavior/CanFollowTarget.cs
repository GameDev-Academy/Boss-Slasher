using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.CustomBehavior
{
    public class CanFollowTarget : Conditional
    {
        public TriggerObserver Observer;
        private bool _isAggro;


        public override void OnStart()
        {
            Observer.TriggerEnter += TriggerEnter;
            Observer.TriggerExit += TriggerExit;
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

        public override void OnReset()
        {
            _isAggro = false;
            Observer.TriggerEnter -= TriggerEnter;
            Observer.TriggerExit -= TriggerExit;
        }
    }
}