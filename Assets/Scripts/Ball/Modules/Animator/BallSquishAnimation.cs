using System;
using System.Collections;
using APIs.Configurable;
using APIs.CoroutineRunner;
using Ball.View;
using Ball.View.Modules.Material;
using Ball.View.Modules.Transform;
using Ball.View.Modules.Transform.TransformModules;
using UnityEngine;

namespace Ball.Modules.Animator
{
    public class BallSquishAnimation : IConfigurable<SquishAnimationConfig>
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly BallView _ballView;

        private SquishAnimationConfig _animationConfiguration;
        
        private Coroutine _animationInstance;

        public BallSquishAnimation(ICoroutineRunner coroutineRunner, BallView ballView)
        {
            _coroutineRunner = coroutineRunner;
            _ballView = ballView;
        }

        public void Configure(SquishAnimationConfig configuration) => 
            _animationConfiguration = configuration;

        public void PlaySquishAnimation(Action onAnimationStarted = null, Action onAnimationFinished = null)
        {
            if(IsAnimationPlayed)
                _coroutineRunner.StopCoroutine(_animationInstance);
            _coroutineRunner.StartCoroutine(AnimateBallSquish(onAnimationStarted, onAnimationFinished));
        }

        private IEnumerator AnimateBallSquish(Action onAnimationStarted, Action onAnimationFinished)
        {
            const int animationSteps = 2;

            onAnimationStarted?.Invoke();

            Color originalMaterialColor = MaterialView.OriginalMaterialColor;
            Vector3 originalScale = ScaleView.OriginalScale;

            float stepDuration = AnimationDuration / animationSteps;
            float pastTime = 0;

            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                float lerpTime = pastTime / stepDuration;

                Color colorToSet = Color.Lerp
                    (originalMaterialColor, SquishColor, lerpTime);
                Vector3 scaleToSet = Vector3.Lerp
                    (originalScale, SquishScale, lerpTime);

                MaterialView.SetMaterialColor(colorToSet);
                ScaleView.SetScale(scaleToSet);
            }


            pastTime = 0;
            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                float lerpTime = pastTime / stepDuration;

                Color colorToSet = Color.Lerp
                    (SquishColor, originalMaterialColor, lerpTime);
                Vector3 scaleToSet = Vector3.Lerp
                    (SquishScale, originalScale, lerpTime);

                MaterialView.SetMaterialColor(colorToSet);
                ScaleView.SetScale(scaleToSet);
            }

            MaterialView.SetMaterialColor(originalMaterialColor);
            ScaleView.SetScale(originalScale);

            onAnimationFinished?.Invoke();
        }

        private bool IsAnimationPlayed => _animationInstance != null;

        private float AnimationDuration => _animationConfiguration.AnimationDuration;
        private Color SquishColor => _animationConfiguration.SquishColor;
        private Vector3 SquishScale => _animationConfiguration.SquishScale;
        private BallTransformView TransformView => _ballView.TransformView;
        private BallScaleView ScaleView => TransformView.ScaleView;
        private BallMaterialView MaterialView => _ballView.MaterialView;
    }
}