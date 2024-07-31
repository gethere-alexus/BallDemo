using Infrastructure.Data.Progress;

namespace Infrastructure.Services.ProgressProvider.API
{
    public interface IProgressReader
    {
        void LoadProgress(GameProgress gameProgress);
    }
}