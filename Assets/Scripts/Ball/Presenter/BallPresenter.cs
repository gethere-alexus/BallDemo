using Ball.Model;
using Ball.Modules.Animator;
using Ball.Modules.Input;
using Ball.Modules.Physics;
using Ball.View;
using Ball.View.Modules.Collision;
using Ball.View.Modules.Force;
using Ball.View.Modules.Transform;
using Ball.View.Modules.Transform.TransformModules;
using Infrastructure.Data.GameConfiguration.Ball.Modules;
using Infrastructure.Factories.EffectsFactory;
using Infrastructure.Services.InputService;
using MVPBase;
using UnityEngine;
using Utils.Extensions;

namespace Ball.Presenter
{
    public class BallPresenter : PresenterBase<BallModel, BallView>
    {
        private BallView _ballView;
        private BallModel _ballModel;

        private BallSquishAnimation _squishAnimation;

        private readonly BallPhysics _ballPhysics;
        private readonly BallInputProcessor _inputProcessor;
        
        private readonly IEffectFactory _effectFactory;

        private bool _isForceTracked;

        public BallPresenter(IInputService inputService, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;

            _inputProcessor = new BallInputProcessor(inputService);
            _ballPhysics = new BallPhysics();
        }

        public override void LinkPresenter(BallModel ballModel, BallView ballView)
        {
            _ballView = ballView;
            _ballModel = ballModel;

            BallInputProcessingConfig inputProcessingConfig = new BallInputProcessingConfig()
                .With(config => config.BaseAppliedForce = ballModel.BaseAppliedForce)
                .With(config => config.MinDistanceToForce = ballModel.MinDistanceToApplyForce)
                .With(config => config.DistanceToForceCoefficient = ballModel.DistanceToForceCoefficient);

            BallPhysicsConfig physicsConfig = new BallPhysicsConfig()
                .With(config => config.LinearDrag = ballModel.LinearDrag)
                .With(config => config.AngularDrag = ballModel.AngularDrag)
                .With(config => config.StretchFactor = ballModel.StretchFactor)
                .With(config => config.MinVelocityToSquish = ballModel.MinVelocityToSquish)
                .With(config => config.SquishFactor = ballModel.SquishFactor);
            
            _inputProcessor.Configure(inputProcessingConfig);
            _ballPhysics.Configure(physicsConfig);
            
            _squishAnimation = new BallSquishAnimation(coroutineRunner: ballView, ballView: ballView);

            ballView.Enabling += Enable;
            ballView.Disabling += Disable;

            Enable();
        }

        public sealed override void Enable()
        {
            _inputProcessor.Enable();

            _inputProcessor.ApplyingForceUpdated += OnApplyingForceUpdated;
            _inputProcessor.ForceCalculated += OnApplyingForceCalculated;

            CollisionView.ObjectCollided += OnCollided;

            TransformView.VelocityUpdateRequested += OnVelocityUpdateRequested;
            TransformView.CurrentVelocityRequested += OnCurrentVelocityRequested;
        }

        public sealed override void Disable()
        {
            _inputProcessor.Disable();

            _inputProcessor.ApplyingForceUpdated -= OnApplyingForceUpdated;
            _inputProcessor.ForceCalculated -= OnApplyingForceCalculated;

            CollisionView.ObjectCollided -= OnCollided;

            TransformView.VelocityUpdateRequested -= OnVelocityUpdateRequested;
            TransformView.CurrentVelocityRequested -= OnCurrentVelocityRequested;
        }

        private void OnCurrentVelocityRequested()
        {
            TransformView.SetVelocity(_ballPhysics.LineVelocity);
            TransformView.SetAngularVelocity(_ballPhysics.AngularVelocity);
        }

        private void OnVelocityUpdateRequested() =>
            _ballPhysics.ApplyFrameVelocityChanges();

        private void OnCollided(Collision collision)
        {
            Vector3 hitNormal = collision.contacts[0].normal;

            ProceedPhysicalHit(collision);
            ProceedSquishOnCollision(hitNormal);
            ProceedHitEffects(hitNormal);
        }

        private void ProceedPhysicalHit(Collision collision) =>
            _ballPhysics.ProceedBallHit(collision);

        private void ProceedSquishOnCollision(Vector3 collisionNormal)
        {
            bool isEnoughVelocityToSquish = _ballPhysics.CurrentVelocity > _ballModel.MinVelocityToSquish;
            if (isEnoughVelocityToSquish)
            {
                Vector3 originalScale = ScaleView.OriginalScale;
                Vector3 squishScale = _ballPhysics.GetSquishScale(originalScale, collisionNormal);

                SquishAnimationConfig animationConfig = new SquishAnimationConfig()
                    .With(config => config.AnimationDuration = _ballModel.SquishDuration)
                    .With(config => config.SquishColor = _ballModel.SquishColor)
                    .With(config => config.SquishScale = squishScale);

                _squishAnimation.Configure(animationConfig);

                _squishAnimation.PlaySquishAnimation(
                    onAnimationStarted: () => _ballPhysics.SetRotationActive(false),
                    onAnimationFinished: () => _ballPhysics.SetRotationActive(true)
                );
            }
        }

        private void ProceedHitEffects(Vector3 hitNormal)
        {
            Vector3 ballPosition = _ballView.TransformView.CurrentPosition;
            Quaternion hitRotation = Quaternion.LookRotation(hitNormal);

            _effectFactory.CreateEffect(EffectType.HitCloud, ballPosition, hitRotation);
        }

        private void OnApplyingForceCalculated() =>
            _ballPhysics.AddForce(_inputProcessor.ApplyingForce, _inputProcessor.ForceAngleDirection);

        private void OnApplyingForceUpdated()
        {
            Vector3 ballPosition = _ballView.TransformView.CurrentPosition;
            float forceScale = _inputProcessor.ApplyingClearForce;

            if (forceScale > CustomPhysics.NoForce)
            {
                forceScale = Mathf.Clamp(
                    value: _inputProcessor.ApplyingClearForce,
                    min: _ballModel.MinDisplayedForceScale,
                    max: _ballModel.MaxDisplayedForceScale);
            }

            ForceView.SetForceView(ballPosition, forceScale, _inputProcessor.ForceAngleDirection);
        }

        private BallTransformView TransformView => _ballView.TransformView;
        private BallScaleView ScaleView => TransformView.ScaleView;
        private BallForceView ForceView => _ballView.ForceView;
        private BallCollisionView CollisionView => _ballView.CollisionView;
    }
}