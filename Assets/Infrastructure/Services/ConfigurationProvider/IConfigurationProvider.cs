using Infrastructure.Services.ConfigurationProvider.API;

namespace Infrastructure.Services.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        public void LoadConfiguration(IConfigReader configReader);
    }
}