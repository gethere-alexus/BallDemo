using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.ProgressProvider.AutoSave
{
    public class AutoSave : IAutoSave
    {
        private readonly IProgressProvider _progressProvider;
        private const float SaveTimeDelay = 2.5f;

        private bool _isEnabled;
        
        public AutoSave(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }

        public void Start()
        {
            _isEnabled = true;
            SaveAutomatically();
        }

        private async void SaveAutomatically()
        {
            while (_isEnabled)
            {
                await UniTask.WaitForSeconds(SaveTimeDelay);
                _progressProvider.SaveProgress();
            }
        }
        public void Stop()
        {
            _isEnabled = false;
            
        }
    }
}