using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.LoadingCurtain;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneProvider
{
    public class SceneProvider : ISceneProvider
    {
        private readonly ILoadingCurtain _loadingCurtain;

        private const float ZeroLoadProgress = 0, FullLoadProgress = 1.0f;

        public SceneProvider(ILoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }

        public void LoadScene(int sceneIndex, Action onSceneLoaded = null) => 
            Load(sceneIndex, onSceneLoaded);

        private async void Load(int sceneIndex, Action sceneLoaded = null)
        {
            if (CurrentSceneIndex != sceneIndex)
            {
                var asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

                NormalizedLoadProgress = ZeroLoadProgress;
                _loadingCurtain.SetProgress(NormalizedLoadProgress);
                
                while (asyncLoad!.isDone == false)
                {
                    NormalizedLoadProgress = asyncLoad.progress; 
                    _loadingCurtain.SetProgress(NormalizedLoadProgress);
                    await UniTask.NextFrame();
                }

                NormalizedLoadProgress = FullLoadProgress;
                _loadingCurtain.SetProgress(NormalizedLoadProgress);
            }
            sceneLoaded?.Invoke();
        }
        
        private int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

        public float NormalizedLoadProgress { get; private set; } = 1.0f;
    }
}