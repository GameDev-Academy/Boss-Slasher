using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private HealthBehaviour _healthBehaviour;

        private void Start()
        {
            _healthBehaviour.Initialize(900);
        }
    }
}