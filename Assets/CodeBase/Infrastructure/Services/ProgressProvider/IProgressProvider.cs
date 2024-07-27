using CodeBase.Infrastructure.Services.ProgressProvider.API;

namespace CodeBase.Infrastructure.Services.ProgressProvider
{
    public interface IProgressProvider
    {
        void LoadProgressToObservers();
        void SaveProgress();
        void RegisterObserver(IProgressReader reader);
    }
}