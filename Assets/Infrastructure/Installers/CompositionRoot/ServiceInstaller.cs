using Infrastructure.Factories.CameraFollowerFactory;
using Infrastructure.Factories.EffectsFactory;
using Infrastructure.Factories.GameStateFactory;
using Infrastructure.Factories.UIFactory;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.Services.GameBuilder;
using Infrastructure.Services.InputService;
using Infrastructure.Services.ProgressProvider;
using Infrastructure.Services.ProgressProvider.AutoSave;
using Infrastructure.Services.ResourceLoader;
using Infrastructure.Services.SceneProvider;
using Infrastructure.StateMachine.GameMachine;
using Zenject;
using PrefabProvider = Infrastructure.Services.PrefabProvider.PrefabProvider;

namespace Infrastructure.Installers.CompositionRoot
{
    public class ServiceInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            BindInputService();

            BindSceneProvider();

            BindProgressProvider();

            BindConfigurationProvider();

            BindResourceLoader();

            BindPrefabLoader();

            BindUIFactory();

            BindEffectFactory();
            
            BindGameStateFactory();

            BindCameraFollowerFactory();
            
            BindGameMachine();

            BindGameBuilder();
        }

        private void BindCameraFollowerFactory() => 
            Container.BindInterfacesTo<CameraFollowerFactory>().AsSingle();

        private void BindProgressProvider()
        {
            Container.BindInterfacesTo<ProgressProvider>().AsSingle();
            Container.BindInterfacesTo<AutoSave>().AsSingle();
        }

        private void BindUIFactory() =>
            Container.BindInterfacesTo<UIFactory>().AsSingle();

        private void BindEffectFactory() => 
            Container.BindInterfacesTo<EffectFactory>().AsSingle();

        private void BindConfigurationProvider() =>
            Container.BindInterfacesTo<ConfigurationProvider>().AsSingle();

        private void BindInputService() =>
            Container.BindInterfacesTo<InputService>()
                .FromNewComponentOnNewGameObject().AsSingle();

        private void BindGameBuilder() =>
            Container.BindInterfacesTo<GameBuilder>().AsSingle();

        private void BindResourceLoader() =>
            Container.BindInterfacesTo<ResourceLoader>().AsSingle();

        private void BindPrefabLoader() =>
            Container.BindInterfacesTo<PrefabProvider>().AsSingle();

        private void BindSceneProvider() =>
            Container.BindInterfacesTo<SceneProvider>().AsSingle();

        private void BindGameStateFactory() =>
            Container.BindInterfacesTo<GameStateFactory>().AsSingle();

        private void BindGameMachine() =>
            Container.Bind<GameStateMachine>().AsSingle();
    }
}