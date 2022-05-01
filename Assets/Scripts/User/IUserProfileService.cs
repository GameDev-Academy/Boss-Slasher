namespace User
{
    public interface IUserProfileService : IService
    {
        UserProfile GetProfile();
    }
}