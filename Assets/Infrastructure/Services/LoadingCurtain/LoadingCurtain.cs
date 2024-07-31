using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Services.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private CanvasGroup _curtainCanvasGroup;

        private const float FadeTime = 0.5f, ProgressTransitionTime = 0.25f;
        private const float ShowedAlpha = 1.0f, HiddenAlpha = 0.0f;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void SetProgress(float newProgress) => 
            SetProgressBar(newProgress);

        public void Show() => 
            SetCurtainOpacity(ShowedAlpha);

        public void Hide() => 
            SetCurtainOpacity(HiddenAlpha);

        private void SetProgressBar(float newProgress) => 
            _progressSlider.DOValue(newProgress, ProgressTransitionTime);

        private void SetCurtainOpacity(float target) => 
            _curtainCanvasGroup.DOFade(target, FadeTime).SetEase(Ease.InExpo);
    }
}