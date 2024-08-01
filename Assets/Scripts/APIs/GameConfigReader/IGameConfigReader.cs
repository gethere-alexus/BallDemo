using Infrastructure.Data.GameConfiguration;

namespace APIs.GameConfigReader
{
    public interface IGameConfigReader
    {
        public void LoadConfiguration(GameConfiguration gameConfiguration);
    }
}