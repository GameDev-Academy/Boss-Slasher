using UniRx;
using UnityEngine;

namespace Battle
{
    public class Portal : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsDungeonPassed => _isDungeonPassed;
        private readonly ReactiveProperty<bool> _isDungeonPassed = new();
        
        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag(Tags.PLAYER))
            {
                _isDungeonPassed.Value = true;
            }
        }
    }
}