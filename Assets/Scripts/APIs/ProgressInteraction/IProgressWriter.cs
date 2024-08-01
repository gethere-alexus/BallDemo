using Infrastructure.Data.GameProgress;

namespace APIs.ProgressInteraction
{
    public interface IProgressWriter : IProgressReader
    {
        void SaveProgress(GameProgress gameProgress);
    }
}