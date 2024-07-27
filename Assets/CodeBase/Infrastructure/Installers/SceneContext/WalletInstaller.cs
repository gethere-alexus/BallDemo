using CodeBase.Sources.Modules.Wallet;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers.SceneContext
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Wallet>().AsSingle();
        }
    }
}