using ConfigurationProviders;

namespace User
{
    public interface IUserProfileService
    {
        UserProfile CreateDefaultUserProfile(IConfigurationProvider configurationProvider);
    }
}