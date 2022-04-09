using Events;
using UnityEngine;

namespace Battle
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventStreams.UserInterface.Publish(new LevelPassEvent(true));
            }
        }
    }
}