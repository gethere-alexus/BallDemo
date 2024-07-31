using Infrastructure.Data.Configurations.Ball;
using Infrastructure.Data.Configurations.Coin;
using Infrastructure.Data.Configurations.Progress;

namespace Infrastructure.Data.Configurations
{
    public class GameConfiguration
    {
        public CoinConfiguration CoinConfiguration;
        public BallConfiguration BallConfiguration;
        public InitialProgress InitialProgress;
    }
}