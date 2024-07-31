using Infrastructure.Data.Configurations;

namespace Infrastructure.Services.ConfigurationProvider.API
{
    public interface IConfigReader
    {
        public void LoadConfiguration(GameConfiguration gameConfiguration);
    }
}