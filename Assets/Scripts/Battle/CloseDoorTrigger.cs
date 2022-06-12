using UniRx;
using UnityEngine;
using UniRx.Triggers;

namespace Battle
{
    public class CloseDoorTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
        private Collider _trigger;

        
        private void Awake()
        {
            _trigger = GetComponent<Collider>();
        }

        private void Start()
        {
            _trigger.OnTriggerEnterAsObservable()
                .Subscribe(_ =>
                {
                    _door.Open(false); 
                    gameObject.SetActive(false);
                })
                .AddTo(this);
        }
    }
}