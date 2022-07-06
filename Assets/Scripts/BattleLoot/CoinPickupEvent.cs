using SimpleEventBus.Events;
using UnityEngine;

namespace BattleLoot
{
    public class CoinPickupEvent : EventBase
    {
        public int Money { get; }
        public Vector3 WorldPosition { get; }
        
        public CoinPickupEvent(Vector3 worldPosition, int money)
        {
            WorldPosition = worldPosition;
            Money = money;
        }
    }
}