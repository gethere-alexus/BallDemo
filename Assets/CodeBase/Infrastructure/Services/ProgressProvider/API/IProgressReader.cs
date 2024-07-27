using CodeBase.Infrastructure.Data.Progress;

namespace CodeBase.Infrastructure.Services.ProgressProvider.API
{
    public interface IProgressReader
    {
        void LoadProgress(GameProgress gameProgress);
    }
}