using ConfigurationProviders;

namespace User
{
    public interface IUserProfileService
    {
        UserProfile CreateDefaultProfile(IConfigurationProvider configurationProvider);
    }
}