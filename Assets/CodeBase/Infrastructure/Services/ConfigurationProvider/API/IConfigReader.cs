using CodeBase.Infrastructure.Data.Configurations;

namespace CodeBase.Infrastructure.Services.ConfigurationProvider.API
{
    public interface IConfigReader
    {
        public void LoadConfiguration(GameConfiguration gameConfiguration);
    }
}