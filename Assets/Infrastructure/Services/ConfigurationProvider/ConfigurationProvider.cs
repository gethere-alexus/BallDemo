using System.IO;
using APIs.GameConfigReader;
using Infrastructure.Data.GameConfiguration;
using Infrastructure.Data.GameConfiguration.Ball;
using Infrastructure.Data.GameConfiguration.CameraFollower;
using Infrastructure.Data.GameConfiguration.Coin;
using Infrastructure.Data.GameConfiguration.Progress;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay;
using Infrastructure.Services.ResourceLoader;
using Infrastructure.StaticData;
using UnityEngine;

namespace Infrastructure.Services.ConfigurationProvider
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IResourceLoader _resourceLoader;
        private const string ConfigsPath = "Configurations";
        
        private readonly GameConfiguration _gameConfiguration;
        
        public ConfigurationProvider(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;

            _gameConfiguration = new GameConfiguration
            {
                BallConfiguration = LoadConfig<BallConfiguration>(ConfigurationsPath.Ball),
                CoinConfiguration = LoadConfig<CoinConfiguration>(ConfigurationsPath.Coin),
                InitialProgress = LoadConfig<InitialProgress>(ConfigurationsPath.InitProgress),
                CameraFollowerConfiguration = LoadConfig<CameraFollowerConfiguration>(ConfigurationsPath.CameraFollower),
                CoinDisplayConfiguration = LoadConfig<CoinDisplayConfiguration>(ConfigurationsPath.CoinDisplay),
            };
        }

        private TConfig LoadConfig<TConfig>(string id) where TConfig : ScriptableObject => 
            _resourceLoader.Load<TConfig>(Path.Combine(ConfigsPath, id));

        public void LoadConfiguration(IGameConfigReader gameConfigReader) => 
            gameConfigReader.LoadConfiguration(_gameConfiguration);
    }
}