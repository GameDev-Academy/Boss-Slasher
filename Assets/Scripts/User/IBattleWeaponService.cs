namespace User
{
    public interface IBattleWeaponService : IService
    {
        void SelectTemporaryWeapon(string id);
        void ResetWeapon();
    }
}