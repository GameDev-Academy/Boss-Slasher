using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleLoot
{
    [RequireComponent(typeof(RectTransform))]
    public class CoinTweener : MonoBehaviour
    {
        [SerializeField] private RectTransform _parentProvider;
        [SerializeField] private GameObject _coinUIPrefab;
        [SerializeField] private RectTransform _target;
        [SerializeField] private int _maxCoins;
        [SerializeField] private int _minCoins;

        [Space]
        [Header("Animation settings")]
        [SerializeField] [Range(1f, 5f)]
        private float minAnimDuration;

        [SerializeField] [Range(1f, 10f)]
        private float maxAnimDuration;
        
        [SerializeField] private Ease easeType;

        private Queue<GameObject> _coinsQueue = new();

        
        private void Awake()
        {
            PrepareCoins(); 
        }

        public void AddCoins(Vector3 collectedCoinPosition)
        {
            Animate(collectedCoinPosition);
        }

        private void PrepareCoins()
        {
            for (int i = 0; i < _maxCoins; i++)
            {
                var coin = Instantiate(_coinUIPrefab, _parentProvider, false);
                coin.SetActive(false);
                _coinsQueue.Enqueue(coin);
            }
        }

        private void Animate(Vector3 collectedCoinPosition)
        {
            var amount = Random.Range(_minCoins, _maxCoins);
            
            for (var i = 0; i < amount; i++)
            {
                if (_coinsQueue.Count > 0)
                {
                    var coin = DequeueActiveCoin();

                    var localPoint = WorldPositionToCanvasLocalPoint(collectedCoinPosition);

                    coin.transform.position = localPoint;

                    var duration = GetRandomDuration();

                    coin.transform.DOMove(_target.position, duration)
                        .SetEase(easeType)
                        .OnComplete(() =>
                        {
                            coin.SetActive(false);
                            _coinsQueue.Enqueue(coin);
                        });
                }
            }
        }

        private GameObject DequeueActiveCoin()
        {
            var coin = _coinsQueue.Dequeue();
            coin.SetActive(true);
            return coin;
        }

        private float GetRandomDuration()
        {
            var duration = Random.Range(minAnimDuration, maxAnimDuration);
            return duration;
        }

        private Vector2 WorldPositionToCanvasLocalPoint(Vector3 collectedCoinPosition)
        {
            var pointInScreenSpace = Camera.main.WorldToScreenPoint(collectedCoinPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,
                pointInScreenSpace, null, out var localPoint);
            return localPoint;
        }
    }
}