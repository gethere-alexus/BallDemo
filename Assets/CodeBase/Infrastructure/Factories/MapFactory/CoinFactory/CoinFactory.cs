using CodeBase.Infrastructure.Data.Configurations;
using CodeBase.Infrastructure.Data.Configurations.Coin;
using CodeBase.Infrastructure.Services.ConfigurationProvider;
using CodeBase.Infrastructure.Services.ConfigurationProvider.API;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Sources.Modules.Coin.Model;
using CodeBase.Sources.Modules.Coin.Presenter;
using CodeBase.Sources.Modules.Coin.View;
using UnityEngine;
using Zenject;
using IPrefabProvider = CodeBase.Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace CodeBase.Infrastructure.Factories.MapFactory.CoinFactory
{
    public class CoinFactory : ICoinFactory, IConfigReader
    {
        private readonly IPrefabProvider _prefabProvider;
        private readonly DiContainer _container;

        private CoinConfiguration _coinConfiguration;

        public CoinFactory(DiContainer container, IPrefabProvider prefabProvider, IConfigurationProvider configurationProvider)
        {
            _container = container;
            _prefabProvider = prefabProvider;
            
            configurationProvider.LoadConfiguration(this);
            
            BindModel();
            BindPresenter();
        }

        public void LoadConfiguration(GameConfiguration gameConfiguration) => 
            _coinConfiguration = gameConfiguration.CoinConfiguration;

        public CoinPresenter CreateCoin(Vector3 at, Transform parent)
        {
            CoinView coin = _prefabProvider.InstantiateWithContainer<CoinView>(_container,PrefabPaths.Coin, at, Quaternion.identity);
            coin.transform.SetParent(parent);
            
            return coin.CoinPresenter;
        }

        private void BindModel()
        {
            CoinModel model = new CoinModel
            {
                CoinsAmount = _coinConfiguration.ReceivingCoins,
            };

            _container.Bind<CoinModel>().FromInstance(model).AsTransient();
        }
        
        private void BindPresenter() => 
            _container.Bind<CoinPresenter>().FromNewComponentSibling().AsTransient();
        
    }
}