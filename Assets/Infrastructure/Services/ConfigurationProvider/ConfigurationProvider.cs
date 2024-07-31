using System.IO;
using Infrastructure.Data.Configurations;
using Infrastructure.Data.Configurations.Ball;
using Infrastructure.Data.Configurations.CameraFollower;
using Infrastructure.Data.Configurations.Coin;
using Infrastructure.Data.Configurations.Progress;
using Infrastructure.Services.ConfigurationProvider.API;
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
                CameraFollowerConfiguration = LoadConfig<CameraFollowerConfiguration>(ConfigurationsPath.CameraFollower)
            };
        }

        private TConfig LoadConfig<TConfig>(string id) where TConfig : ScriptableObject => 
            _resourceLoader.Load<TConfig>(Path.Combine(ConfigsPath, id));

        public void LoadConfiguration(IConfigReader configReader) => 
            configReader.LoadConfiguration(_gameConfiguration);
    }
}