using Infrastructure.Data.GameConfiguration.Ball;
using Infrastructure.Data.GameConfiguration.CameraFollower;
using Infrastructure.Data.GameConfiguration.Coin;
using Infrastructure.Data.GameConfiguration.Progress;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay;

namespace Infrastructure.Data.GameConfiguration
{
    public class GameConfiguration
    {
        public CoinConfiguration CoinConfiguration;
        public BallConfiguration BallConfiguration;
        public InitialProgress InitialProgress;
        public CameraFollowerConfiguration CameraFollowerConfiguration;
        public CoinDisplayConfiguration CoinDisplayConfiguration;
    }
}