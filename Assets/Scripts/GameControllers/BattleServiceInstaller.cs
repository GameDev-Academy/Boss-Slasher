using BattleLoot;
using Player;
using UnityEngine;
using User;

namespace GameControllers
{
    /// <summary>
    /// The class is responsible for initializing battle services
    /// </summary>
    public class BattleServiceInstaller : MonoBehaviour
    {
        [SerializeField] private PopupTextService _popupTextController;
        [SerializeField] private FlyingCoinsService _flyingCoinsService;
        
        public void RegisterService()
        {
            var serviceLocator = ServiceLocator.Instance;
            serviceLocator.RegisterSingle<IDungeonMoneyService>(new DungeonMoneyService());
            serviceLocator.RegisterSingle<IPopupTextService>(_popupTextController);
            serviceLocator.RegisterSingle<IFlyingCoinsService>(_flyingCoinsService);

            var weaponService = serviceLocator.GetSingle<IWeaponsService>();
            serviceLocator.RegisterSingle<IBattleWeaponService>(new BattleWeaponService(weaponService));
        }
    }
}