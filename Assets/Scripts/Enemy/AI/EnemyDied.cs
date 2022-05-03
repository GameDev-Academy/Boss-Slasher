using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.AI
{
    /// <summary>
    /// Проверяет умер ли Енеми
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public class EnemyDied : Action 
    {
        [SerializeField]
        private HealthBehaviour _healthBehaviour; 
        
        public override TaskStatus OnUpdate()
        {
            if (_healthBehaviour.IsDead.Value)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}