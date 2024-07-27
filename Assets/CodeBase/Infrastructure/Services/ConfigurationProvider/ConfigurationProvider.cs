using System.IO;
using CodeBase.Infrastructure.Data.Configurations;
using CodeBase.Infrastructure.Data.Configurations.Ball;
using CodeBase.Infrastructure.Data.Configurations.Coin;
using CodeBase.Infrastructure.Data.Configurations.Progress;
using CodeBase.Infrastructure.Services.ConfigurationProvider.API;
using CodeBase.Infrastructure.Services.ResourceLoader;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ConfigurationProvider
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
            };
        }

        private TConfig LoadConfig<TConfig>(string id) where TConfig : ScriptableObject => 
            _resourceLoader.Load<TConfig>(Path.Combine(ConfigsPath, id));

        public void LoadConfiguration(IConfigReader configReader) => 
            configReader.LoadConfiguration(_gameConfiguration);
    }
}