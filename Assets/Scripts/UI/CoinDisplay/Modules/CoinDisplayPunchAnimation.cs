using System.Collections;
using APIs.Configurable;
using APIs.CoroutineRunner;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay.Modules;
using UI.CoinDisplay.View;
using UnityEngine;

namespace UI.CoinDisplay.Modules
{
    public class CoinDisplayPunchAnimation : IConfigurable<CoinDisplayAnimationConfig>
    {
        private readonly ICoroutineRunner _animationRunner;
        private readonly CoinBalanceView _coinBalanceView;

        private float _punchScaleCoefficient;
        private float _punchTime;

        private Coroutine _punchAnimation;

        public CoinDisplayPunchAnimation(ICoroutineRunner animationRunner, CoinBalanceView coinBalanceView)
        {
            _animationRunner = animationRunner;
            _coinBalanceView = coinBalanceView;
        }

        public void Configure(CoinDisplayAnimationConfig configuration)
        {
            _punchTime = configuration.PunchTime;
            _punchScaleCoefficient = configuration.PunchScaleCoefficient;
            
        }

        public void PlayPunchAnimation()
        {
            if(_punchAnimation != null)
                _animationRunner.StopCoroutine(_punchAnimation);
            _punchAnimation = _animationRunner.StartCoroutine(AnimatePunch());

        }

        private IEnumerator AnimatePunch()
        {
            const float punchSteps = 3;
            float originalFont = OriginalFontSize;

            float stepTime = _punchTime / punchSteps;
            
            float pastTime = 0;
            while (pastTime <=stepTime)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _coinBalanceView.SetFontSize(Mathf.Lerp(originalFont, originalFont * _punchScaleCoefficient, pastTime / stepTime));
            }

            yield return new WaitForSeconds(stepTime);

            pastTime = 0;
            while (pastTime <=stepTime)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _coinBalanceView.SetFontSize(Mathf.Lerp(originalFont * _punchScaleCoefficient, originalFont, pastTime / stepTime));
            }

            _coinBalanceView.SetFontSize(originalFont);
        }

        private float OriginalFontSize => _coinBalanceView.OriginalFontSize;
    }
}