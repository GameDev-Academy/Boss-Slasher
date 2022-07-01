using UnityEngine;

namespace Battle
{
    public class CloseDoorTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
        

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.PLAYER))
            {
                _door.SetOpenedState(false); 
                gameObject.SetActive(false);
            }
        }
    }
}