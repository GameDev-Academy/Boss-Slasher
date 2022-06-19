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
        private ITargetProvider _targetProvider;
        private bool _isAggro;
        
        public override void OnAwake()
        {
            base.OnAwake();
            _targetProvider = gameObject.GetComponent<ITargetProvider>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_targetProvider.HasAnyTarget())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}