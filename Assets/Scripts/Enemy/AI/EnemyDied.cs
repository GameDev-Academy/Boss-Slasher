using System;
using BehaviorDesigner.Runtime.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy.AI
{
    /// <summary>
    /// Проверяет умер ли Енеми
    /// </summary>
    [UsedImplicitly]
    [Serializable]
    public class EnemyDied : Conditional 
    {
        [FormerlySerializedAs("_healthBehaviour")]
        [SerializeField]
        private HealthHandler _healthHandler; 
        
        public override TaskStatus OnUpdate()
        {
            return _healthHandler.IsDead.Value ? 
                TaskStatus.Success : 
                TaskStatus.Running;
        }
    }
}