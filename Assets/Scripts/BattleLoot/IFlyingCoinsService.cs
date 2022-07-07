using UnityEngine;

namespace BattleLoot
{
    public interface IFlyingCoinsService : IService
    {
        void Show(Vector3 position, int moneyInCoin);
    }
}