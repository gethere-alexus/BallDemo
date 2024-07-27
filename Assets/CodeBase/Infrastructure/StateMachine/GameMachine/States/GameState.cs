using CodeBase.Infrastructure.Services.LoadingCurtain;
using CodeBase.Infrastructure.Services.ProgressProvider;
using CodeBase.Infrastructure.Services.ProgressProvider.AutoSave;
using CodeBase.Infrastructure.Services.SceneProvider;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.StateMachine.GameMachine.States
{
    public class GameState : IGameState
    {
        private readonly ISceneProvider _sceneProvider;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAutoSave _autoSave;
        private readonly IProgressProvider _progressProvider;

        public GameState(ISceneProvider sceneProvider, ILoadingCurtain loadingCurtain, IAutoSave autoSave, IProgressProvider progressProvider)
        {
            _sceneProvider = sceneProvider;
            _loadingCurtain = loadingCurtain;
            _autoSave = autoSave;
            _progressProvider = progressProvider;
        }

        public void Enter() => 
            _sceneProvider.LoadScene(SceneIDs.Game, OnGameLoaded);

        private void OnGameLoaded()
        {
            _progressProvider.LoadProgressToObservers();
            _autoSave.Start();
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
            _autoSave.Stop();
        }
    }
}