using Infrastructure.Data.GameProgress;

namespace APIs.ProgressInteraction
{
    public interface IProgressReader
    {
        void LoadProgress(GameProgress gameProgress);
    }
}