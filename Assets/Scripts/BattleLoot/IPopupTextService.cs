using UnityEngine;

namespace BattleLoot
{
    public interface IPopupTextService : IService
    {
        void Show(Vector3 worldPosition, string text);
    }
}