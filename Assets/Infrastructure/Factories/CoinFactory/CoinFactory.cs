using APIs.GameConfigReader;
using Coin.Model;
using Coin.Presenter;
using Coin.View;
using Infrastructure.Data.GameConfiguration;
using Infrastructure.Data.GameConfiguration.Coin;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.StaticData;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using IPrefabProvider = Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace Infrastructure.Factories.CoinFactory
{
    public class CoinFactory : ICoinFactory, IGameConfigReader
    {
        private readonly IPrefabProvider _prefabProvider;
        private readonly DiContainer _container;

        private CoinConfiguration _coinConfiguration;

        public CoinFactory(DiContainer container, IPrefabProvider prefabProvider, IConfigurationProvider configurationProvider)
        {
            _container = container;
            _prefabProvider = prefabProvider;
            
            configurationProvider.LoadConfiguration(this);
        }

        public void LoadConfiguration(GameConfiguration gameConfiguration) => 
            _coinConfiguration = gameConfiguration.CoinConfiguration;

        public CoinPresenter CreateCoin(Vector3 at, Transform parent)
        {
            CoinModel coinModel = ConstructModel();
            
            CoinView coinView = _prefabProvider.InstantiateWithContainer<CoinView>(_container,PrefabPaths.Coin, at,  Quaternion.identity);
            coinView.transform.SetParent(parent);

            CoinPresenter coinPresenter = _container.Instantiate<CoinPresenter>()
                .With(presenter => presenter.LinkPresenter(coinModel, coinView));

            return coinPresenter;
        }

        private CoinModel ConstructModel()
        {
            CoinConfiguration config = _coinConfiguration;

            CoinModel model = new CoinModel()
                .With(model => model.SetClaimingCoins(config.ReceivingCoins))
                .With(model => model.SetAnimationConfiguration(config.AnimationTimeToDisappear))
                .With(model => model.SetRotationSpeed(config.RotationSpeed));
            
            return model;
        }
        
        
    }
}