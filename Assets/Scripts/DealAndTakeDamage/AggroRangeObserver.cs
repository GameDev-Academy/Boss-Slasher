using System;
using UnityEngine;

namespace DealAndTakeDamage
{
    /// <summary>
    /// Уведомляет о попадании коллайдера в радиус того коллайдера, на которого повешен этот скрипт
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class AggroRangeObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
        
        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnter?.Invoke(collider);
        }
        
        private void OnTriggerExit(Collider collider)
        {
            TriggerExit?.Invoke(collider);
        }
    }
}