using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// The class displays money in the dungeon on the UI
    /// </summary>
    public class BattleMoneyPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private int _animationTime = 1;
        
        private CompositeDisposable _subscriptions;
        private int _moneyAmount;
        private float _currentMoney;
        private Coroutine _moneyTextCoroutine;
        private IPopupTextService _popupText;
        private IFlyingCoinsService _flyingCoins;
        private IDungeonMoneyService _dungeonMoney;
        
        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<CoinPickupEvent>(CoinPickupEventHandler),
            };

            _dungeonMoney = ServiceLocator.Instance.GetSingle<IDungeonMoneyService>();
            _popupText = ServiceLocator.Instance.GetSingle<IPopupTextService>();
            _flyingCoins = ServiceLocator.Instance.GetSingle<IFlyingCoinsService>();
            
            _moneyText.text = _dungeonMoney.Money.Value.ToString(); //todo Спросить Никиту, как сделать текст на winUI лучше
        }
        
        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }

        private void CoinPickupEventHandler(CoinPickupEvent eventData)
        {
            var worldPosition = eventData.WorldPosition;
            var moneyInCoin = eventData.Money;

            _popupText.Show(worldPosition, moneyInCoin.ToString());
            _flyingCoins.Show(worldPosition, moneyInCoin);

            StartShowMoneyCoroutine(_dungeonMoney.Money.Value, _moneyTextCoroutine);
        }

        private void StartShowMoneyCoroutine(int money, Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            _moneyTextCoroutine = StartCoroutine(ShowChangingMoneyAnimation(money));
        }

        private IEnumerator ShowChangingMoneyAnimation(float newMoney)
        {
            var currentTime = 0f;

            while (currentTime < _animationTime)
            {
                var moneyPerFrame = UpgradeMoneyPerFrame(newMoney, ref currentTime, _currentMoney);

                _moneyText.text = moneyPerFrame.ToString();
                _currentMoney = moneyPerFrame;

                yield return null;
            }
        }

        private int UpgradeMoneyPerFrame(float newMoney, ref float currentTime, float currentMoney)
        {
            var lerpMoney = Mathf.Lerp(currentMoney, newMoney, currentTime / _animationTime);

            var moneyPerFrame = Mathf.CeilToInt(lerpMoney);
            currentTime += Time.deltaTime;

            return moneyPerFrame;
        }
    }
}