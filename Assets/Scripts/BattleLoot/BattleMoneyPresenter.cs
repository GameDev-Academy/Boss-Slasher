using TMPro;
using UniRx;
using UnityEngine;

namespace BattleLoot
{
    /// <summary>
    /// Класс, отвечающий за отображение на UI денег полученных в подземелье
    /// </summary>
    public class BattleMoneyPresenter : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI _coinsUIValue;

        private ILootDataService _lootData;

        private void Awake()
        {
            _lootData = ServiceLocator.Instance.GetSingle<ILootDataService>();
            
            _lootData.Money
                .Subscribe(_ => _coinsUIValue.text = _lootData.Money.Value.ToString())
                .AddTo(this);
        }
    }
}