using System;
using UnityEngine;

namespace Battle
{
    public class CloseDoorTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
        private void OnTriggerEnter(Collider collider)
        {
            _door.IsOpen(false);
            gameObject.SetActive(false);
        }
    }
}