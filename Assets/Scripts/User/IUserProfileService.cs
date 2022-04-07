namespace User
{
    public interface IUserProfileService
    {
        void Initialize(ICharacteristicsService characteristicsService, IMoneyService moneyService);
        UserProfile LoadOrCreateDefaultProfile();
    }
}