using APIs.GameConfigReader;
using Infrastructure.Data.GameConfiguration;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay.Modules;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.StaticData;
using UI.CoinDisplay.Model;
using UI.CoinDisplay.Presenter;
using UI.CoinDisplay.View;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using IPrefabProvider = Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace Infrastructure.Factories.UIFactory
{
    public class UIFactory : IUIFactory, IGameConfigReader
    {
        private readonly DiContainer _container;
        private readonly IPrefabProvider _assetsProvider;

        private Canvas _rootInstance;
        private CoinDisplayConfiguration _balanceDisplayConfig;

        public UIFactory(DiContainer container,IPrefabProvider assetsProvider, IConfigurationProvider configurationProvider)
        {
            _container = container;
            _assetsProvider = assetsProvider;
            
            configurationProvider.LoadConfiguration(this);
        }

        public void LoadConfiguration(GameConfiguration gameConfiguration)
        {
            _balanceDisplayConfig = gameConfiguration.CoinDisplayConfiguration;
        }

        public void CreateBalanceDisplay()
        {
            CoinDisplayAnimationConfig animConfig = _balanceDisplayConfig.AnimationConfig;
            CoinDisplayAddConfig addConfig = _balanceDisplayConfig.AddConfig;
            
            CoinBalanceModel model = new CoinBalanceModel()
                .With(model => model.SetAnimationConfig(
                    punchScaleCoefficient: animConfig.PunchScaleCoefficient,
                    punchTime: animConfig.PunchTime))
                .With(model => model.SetAddConfig(
                    timeToAddCoin: addConfig.TimeToAddCoin,
                    punchStep: addConfig.PunchStep));
            
            CoinBalanceView view = _assetsProvider.InstantiateWithZenject<CoinBalanceView>(PrefabPaths.WalletPanel, GetRoot().transform);
            
            CoinBalancePresenter presenter = _container.Instantiate<CoinBalancePresenter>()
                .With(presenter => presenter.LinkPresenter(model, view));
        }

        private Canvas CreateRoot()
        {
            _rootInstance = _assetsProvider.Instantiate<Canvas>(PrefabPaths.UIRoot);
            return _rootInstance;
        }

        private Canvas GetRoot()
        {
            if (_rootInstance == null)
                CreateRoot();
            
            return _rootInstance;
        }
    }
}