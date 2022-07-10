using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleLoot
{
    /// <summary>
    /// Class is responsible for getting money from loot
    /// </summary>
    public class AddMoneyAction : LootAction
    {
        [SerializeField] private int _minMoney = 1;
        [SerializeField] private int _maxMoney = 10;
        
        private IDungeonMoneyService _dungeonMoney;

        private void Awake()
        {
            _dungeonMoney = ServiceLocator.Instance.GetSingle<IDungeonMoneyService>();
        }

        public override void Execute(Collider collider)
        {
            var money = Random.Range(_minMoney, _maxMoney);
            _dungeonMoney.Collect(money);

            EventStreams.UserInterface.Publish(new CoinPickupEvent(gameObject.transform.position, money));
        }
    }
}