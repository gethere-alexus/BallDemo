using Zenject;

namespace Infrastructure.Installers.SceneContext
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Wallet.Wallet>().AsSingle();
        }
    }
}