using System;

namespace Infrastructure.Services.SceneProvider
{
    public interface ISceneProvider
    {
        void LoadScene(int sceneIndex, Action onSceneLoaded = null);
        
        float NormalizedLoadProgress { get; }
    }
}