using System;
using UniRx;
using UnityEngine;
using UniRx.Triggers;

namespace Battle
{
    public class CloseDoorTrigger : MonoBehaviour
    {
        private const string Player = "Player";
        [SerializeField] private Door _door;
        

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Player))
            {
                _door.SetOpenedState(false); 
                gameObject.SetActive(false);
            }
        }
    }
}