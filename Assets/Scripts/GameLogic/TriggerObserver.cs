using System;
using UnityEngine;

namespace Enemy.CustomBehavior
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider eventData)
        {
            TriggerEnter?.Invoke(eventData);
        }

        private void OnTriggerExit(Collider eventData)
        {
            TriggerExit?.Invoke(eventData);
        }
    }
}