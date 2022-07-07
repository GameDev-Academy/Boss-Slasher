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
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _parentProvider;
        [SerializeField] private RectTransform _coinUIPrefab;
        [SerializeField] private RectTransform _target;
        [SerializeField] private int _coinsCont = 30;
        
        [Space] [Header("Animation settings")]
        [SerializeField] [Range(0.5f, 2f)]
        private float _minAnimDuration;
        
        [SerializeField] [Range(2f, 5f)]
        private float _maxAnimDuration;
        
        [SerializeField] private Ease easeType;

        private MonoBehaviourPool<RectTransform> _coinsPool;


        private void Awake()
        {
            if (_canvas == null)
            {
                _canvas = GetComponent<Canvas>();
            }

            _coinsPool = new MonoBehaviourPool<RectTransform>(_coinUIPrefab, _parentProvider, _coinsCont);
        }

        public void Show(Vector3 collectedCoinPosition, int moneyInCoin)
        {
            Animate(collectedCoinPosition, moneyInCoin);
        }

        private void Animate(Vector3 worldCoinPosition, int moneyInCoin)
        {
            for (var i = 0; i < moneyInCoin; i++)
            {
                var coin = _coinsPool.Take();
                var screenPoint = Camera.main.WorldToScreenPoint(worldCoinPosition);
                coin.position = screenPoint;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas.transform as RectTransform,
                    _target.position, null, out var initializePosition);

                var duration = GetRandomDuration();

                coin.DOMove(initializePosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() => { _coinsPool.Release(coin); });
            }
        }

        private float GetRandomDuration()
        {
            var duration = Random.Range(_minAnimDuration, _maxAnimDuration);
            return duration;
        }
    }
}