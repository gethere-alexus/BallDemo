using APIs.GameConfigReader;

namespace Infrastructure.Services.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        public void LoadConfiguration(IGameConfigReader gameConfigReader);
    }
}