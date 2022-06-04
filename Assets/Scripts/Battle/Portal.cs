using System;
using Events;
using UnityEngine;

namespace Battle
{
    public class Portal : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                EventStreams.UserInterface.Publish(new LevelPassEvent(true));
            }
        }
    }
}