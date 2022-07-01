using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleLoot
{
    /// <summary>
    /// The class displays money in the dungeon on the UI
    /// </summary>
    public class BattleMoneyPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyBattleUIValue;

        [SerializeField] private TextMeshProUGUI _moneyWinUIValue;

        [SerializeField] private int _animationTime = 1;
        private float _currentMoney;

        private ILootDataService _lootData;

        private void Awake()
        {
            _lootData = ServiceLocator.Instance.GetSingle<ILootDataService>();

            _lootData.Money
                .Subscribe(_ =>
                {
                    StartCoroutine(WaitUntilCoroutineCompleted(_lootData.Money.Value));
                    
                    ShowMoneyOnWinUI();
                })
                .AddTo(this);
        }

        private IEnumerator WaitUntilCoroutineCompleted(int newMoney)
        {
            yield return StartCoroutine(CalculateMoney(newMoney));
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
            var lerpMoney = Mathf.Lerp(currentMoney, newMoney, currentTime / _animationTime);

            var moneyPerFrame = Mathf.RoundToInt(lerpMoney);
            currentTime += Time.deltaTime;

            return moneyPerFrame;
        }

        private void AddMoneyOnUI(int moneyPerFrame)
        {
            _moneyBattleUIValue.text = moneyPerFrame.ToString();
        }

        private void ShowMoneyOnWinUI()
        {
            _moneyWinUIValue.text = _lootData.Money.Value.ToString();
        }
    }
}