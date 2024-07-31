using Infrastructure.Services.PrefabProvider;
using Infrastructure.StaticData;
using UnityEngine;
using Wallet.View;

namespace Infrastructure.Factories.UIFactory
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