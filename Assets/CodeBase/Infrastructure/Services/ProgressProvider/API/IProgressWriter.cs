using CodeBase.Infrastructure.Data.Progress;

namespace CodeBase.Infrastructure.Services.ProgressProvider.API
{
    public interface IProgressWriter : IProgressReader
    {
        void SaveProgress(GameProgress gameProgress);
    }
}