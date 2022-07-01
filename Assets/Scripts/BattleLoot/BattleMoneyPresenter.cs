using System;
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
        [SerializeField] private TextMeshProUGUI _coinsBattleUIValue;
        [SerializeField] private TextMeshProUGUI _coinsWinUIValue;
        [SerializeField] private int _animationTime = 1;
        private float _currentMoney;

        private ILootDataService _lootData;

        private void Awake()
        {
            _lootData = ServiceLocator.Instance.GetSingle<ILootDataService>();

            _lootData.Money
                .Subscribe(_ =>
                {
                    StartCoroutine(CalculateMoney(_lootData.Money.Value));

                    ShowMoneyOnWinUI();
                })
                .AddTo(this);
        }

        private IEnumerator CalculateMoney(float newMoney)
        {
            var currentTime = 0f;

            while (currentTime < _animationTime)
            {
                var moneyPerFrame = UpgradeMoneyPerFrame(newMoney, ref currentTime, _currentMoney);

                AddMoneyOnUI(moneyPerFrame);

                yield return null;
            }

            _currentMoney = newMoney;
        }

        private int UpgradeMoneyPerFrame(float newMoney, ref float currentTime, float currentMoney)
        {
            var interpolatingScore = Mathf.Lerp(currentMoney, newMoney, currentTime / _animationTime);

            var moneyPerFrame = Mathf.FloorToInt(interpolatingScore);
            currentTime += Time.deltaTime;

            return moneyPerFrame;
        }

        private void AddMoneyOnUI(int incrementScorePerFrame)
        {
            _coinsBattleUIValue.text = incrementScorePerFrame.ToString();
        }

        private void ShowMoneyOnWinUI()
        {
            _coinsWinUIValue.text = _lootData.Money.Value.ToString();
        }
    }
}