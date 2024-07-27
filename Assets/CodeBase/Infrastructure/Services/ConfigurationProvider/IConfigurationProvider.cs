using CodeBase.Infrastructure.Services.ConfigurationProvider.API;

namespace CodeBase.Infrastructure.Services.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        public void LoadConfiguration(IConfigReader configReader);
    }
}