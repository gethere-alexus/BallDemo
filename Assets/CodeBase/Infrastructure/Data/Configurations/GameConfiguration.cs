using CodeBase.Infrastructure.Data.Configurations.Ball;
using CodeBase.Infrastructure.Data.Configurations.Coin;
using CodeBase.Infrastructure.Data.Configurations.Progress;

namespace CodeBase.Infrastructure.Data.Configurations
{
    public class GameConfiguration
    {
        public CoinConfiguration CoinConfiguration;
        public BallConfiguration BallConfiguration;
        public InitialProgress InitialProgress;
    }
}