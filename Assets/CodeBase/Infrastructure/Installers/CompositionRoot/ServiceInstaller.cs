using CodeBase.Infrastructure.Factories.EffectsFactory;
using CodeBase.Infrastructure.Factories.GameStateFactory;
using CodeBase.Infrastructure.Factories.UIFactory;
using CodeBase.Infrastructure.Services.ConfigurationProvider;
using CodeBase.Infrastructure.Services.GameBuilder;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.ProgressProvider;
using CodeBase.Infrastructure.Services.ProgressProvider.AutoSave;
using CodeBase.Infrastructure.Services.ResourceLoader;
using CodeBase.Infrastructure.Services.SceneProvider;
using CodeBase.Infrastructure.StateMachine.GameMachine;
using Zenject;
using PrefabProvider = CodeBase.Infrastructure.Services.PrefabProvider.PrefabProvider;

namespace CodeBase.Infrastructure.Installers.CompositionRoot
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

            BindGameMachine();

            BindGameBuilder();
        }

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