namespace CodeBase.Infrastructure.Services.LoadingCurtain
{
    public interface ILoadingCurtain 
    {
        void Show();
        void Hide();
        void SetProgress(float newProgress);
    }
}