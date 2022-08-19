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
        [SerializeField] private EnemyHealth _enemyHealth;

        public override TaskStatus OnUpdate()
        {
            return _enemyHealth.IsDead.Value ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}