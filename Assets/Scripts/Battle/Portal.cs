using Events;
using UnityEngine;

namespace Battle
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                EventStreams.UserInterface.Publish(new LevelPassEvent(true));
            }
        }
    }
}