namespace User
{
    public interface IUserProfileFactory
    {
        UserProfile CreateOrLoadProfile();
    }
}