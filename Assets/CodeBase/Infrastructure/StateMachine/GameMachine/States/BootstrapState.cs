using CodeBase.Infrastructure.Services.LoadingCurtain;
using CodeBase.Infrastructure.Services.SceneProvider;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.StateMachine.GameMachine.States
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneProvider _sceneProvider;
        private readonly ILoadingCurtain _loadingCurtain;

        public BootstrapState(GameStateMachine gameStateMachine, ISceneProvider sceneProvider, ILoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneProvider = sceneProvider;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter() => 
            _sceneProvider.LoadScene(SceneIDs.Bootstrap, OnBootstrapped);

        private void OnBootstrapped()
        {
            _loadingCurtain.Show();
            _gameStateMachine.Enter<GameState>();
        }
        

        public void Exit()
        {
            
        }
    }
}