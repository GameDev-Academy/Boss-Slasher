using Events;
using UniRx;
using UnityEngine;

namespace GameControllers
{
    public class PortalController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _portal;
        
        private CompositeDisposable _subscriptions;

        private void Start()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<BossDefeatedEvent>(BossDefeatedHandler)
            };
        }
        private void BossDefeatedHandler(BossDefeatedEvent eventData)
        {
            _portal.SetActive(true);
        }
        
        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}