using Infrastructure.Data.Progress;

namespace Infrastructure.Services.ProgressProvider.API
{
    public interface IProgressWriter : IProgressReader
    {
        void SaveProgress(GameProgress gameProgress);
    }
}