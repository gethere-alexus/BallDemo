using CodeBase.Infrastructure.Services.PrefabProvider;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Sources.Modules.Wallet;
using CodeBase.Sources.Modules.Wallet.View;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IPrefabProvider _assetsProvider;

        private Canvas _rootInstance;

        public UIFactory(IPrefabProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public void CreateBalanceDisplay()
        {
            _assetsProvider.InstantiateWithZenject<CoinBalanceView>(PrefabPaths.WalletPanel, GetRoot().transform);
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