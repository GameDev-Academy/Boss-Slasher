using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemy.AI
{
    /// <summary>
    /// Разворачивает объект к цели
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public sealed class LookAtTarget : EnemyAction
    {
        [SerializeField]
        private float _speedRotation = 2f;
        private float _time;
        
        public override TaskStatus OnUpdate()
        {
            var targetPosition = Target.transform.position;
            var position = transform.position;
            var relativePosition = new Vector3(targetPosition.x - position.x,0f, targetPosition.z - position.z);
            var targetRotation = Quaternion.LookRotation(relativePosition);
            _time += Time.deltaTime * _speedRotation;

            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation, _time);

            return _time > 1 ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            base.OnEnd();
            _time = 0f;
        }
    }
}