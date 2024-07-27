using CodeBase.Infrastructure.Bootstrap;
using CodeBase.Infrastructure.Factories.GameStateFactory;
using CodeBase.Infrastructure.Services.LoadingCurtain;
using CodeBase.Infrastructure.StateMachine.GameMachine;
using CodeBase.Infrastructure.StateMachine.GameMachine.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers.CompositionRoot
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings() => 
            Container.BindInterfacesTo<GameInstaller>().FromInstance(this).AsSingle();

        public void Initialize()
        {
            InstallLoadingCurtain();
            
            GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
            IGameStateFactory stateFactory = Container.Resolve<IGameStateFactory>();

            gameStateMachine.AddState(stateFactory.CreateState<BootstrapState>());
            gameStateMachine.AddState(stateFactory.CreateState<GameState>());
            
            Game gameInstance = new Game(gameStateMachine);
            Container.Bind<Game>().FromInstance(gameInstance).AsSingle();
        }

        private void InstallLoadingCurtain()
        {
            LoadingCurtain curtainInstance = Container.InstantiatePrefabForComponent<LoadingCurtain>(_loadingCurtain);
            Container.BindInterfacesTo<LoadingCurtain>().FromInstance(curtainInstance).AsSingle();
        }
    }
}