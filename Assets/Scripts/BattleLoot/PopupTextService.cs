using System.Collections;
using GameControllers;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for popup spawning
    /// </summary>
    public class PopupTextService : MonoBehaviour, IPopupTextService
    {
        [SerializeField] private Popup _popupPrefab;
        [SerializeField] private Transform _parentProvider;
        [SerializeField] private int _popupPoolCount = 5;
        [SerializeField] private int _timeLifePopup = 1;

        private int _money;
        private MonoBehaviourPool<Popup> _popupPool;
        private WaitForSeconds _waitForSeconds;


        private void Awake()
        {
            _popupPool = new MonoBehaviourPool<Popup>(_popupPrefab, _parentProvider, _popupPoolCount);
            _waitForSeconds = new WaitForSeconds(_timeLifePopup);
        }

        public void Show(Vector3 worldPosition, string text)
        {
            var popup = _popupPool.Take();
            
            popup.gameObject.transform.position = worldPosition;
            popup.Text.text = text;
            
            StartCoroutine(TimeToRelease(popup));
        }
        
        private IEnumerator TimeToRelease(Popup popup)
        {
            yield return _waitForSeconds;
            _popupPool.Release(popup);
        }
    }
}