using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemy.CustomBehavior
{
    /// <summary>
    /// Делает проверку вошел ли игрок в зону аггро
    /// </summary>
    [UsedImplicitly]
    public class CanFollowTarget : Conditional
    {
        public TriggerObserver Observer;
        private bool _isAggro;


        public override void OnStart()
        {
            base.OnStart();
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
            else
            {
                return TaskStatus.Failure;
            }
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