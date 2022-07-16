using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleLoot
{
    /// <summary>
    /// The class is responsible for the flight of coins on the canvas
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class FlyingCoinsService : MonoBehaviour, IFlyingCoinsService
    {
        [SerializeField] private RectTransform _parentProvider;
        [SerializeField] private RectTransform _coinUIPrefab;
        [SerializeField] private RectTransform _target;
        [SerializeField] private int _coinsCont = 30;
        
        [Space] [Header("Animation settings")]
        [SerializeField] [Range(0.5f, 2f)]
        private float _minAnimDuration;
        
        [SerializeField] [Range(2f, 5f)]
        private float _maxAnimDuration;
        
        [SerializeField] private Ease _easeType;

        private MonoBehaviourPool<RectTransform> _coinsViewPool;
        private Camera _mainCamera;


        private void Awake()
        {
            _mainCamera = Camera.main;
            _coinsViewPool = new MonoBehaviourPool<RectTransform>(_coinUIPrefab, _parentProvider, _coinsCont);
        }

        public void Show(Vector3 collectedCoinPosition, int moneyInCoin)
        {
            Animate(collectedCoinPosition, moneyInCoin);
        }

        private void Animate(Vector3 worldCoinPosition, int moneyInCoin)
        {
            for (var i = 0; i < moneyInCoin; i++)
            {
                var coinView = _coinsViewPool.Take();
                
                var screenPoint = _mainCamera.WorldToScreenPoint(worldCoinPosition);
                var coinParent = coinView.parent as RectTransform;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(coinParent, screenPoint, null,
                    out var startPosition);
                
                coinView.position = startPosition;
                
                RectTransformUtility.ScreenPointToWorldPointInRectangle(coinParent,
                    _target.anchoredPosition, null, out var targetPosition);
                
                var duration = GetRandomDuration();

                coinView.DOAnchorPos3D(targetPosition, duration)
                    .SetEase(_easeType)
                    .OnComplete(() => { _coinsViewPool.Release(coinView);});
            }
        }

        private float GetRandomDuration()
        {
            var duration = Random.Range(_minAnimDuration, _maxAnimDuration);
            return duration;
        }
    }
}