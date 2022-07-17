namespace User
{
    public interface IUserProfileService : IService
    {
        UserProfile GetCurrentProfile();
        UserProfile CreateNewOrGetLastProfile();
    }
}