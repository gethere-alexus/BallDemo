using Infrastructure.Services.ProgressProvider.API;

namespace Infrastructure.Services.ProgressProvider
{
    public interface IProgressProvider
    {
        void LoadProgressToObservers();
        void SaveProgress();
        void RegisterObserver(IProgressReader reader);
    }
}