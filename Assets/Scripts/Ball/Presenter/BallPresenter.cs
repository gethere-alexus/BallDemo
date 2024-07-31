using Ball.Model;
using Ball.Modules.Input;
using Ball.Modules.Physics;
using Ball.View;
using Ball.View.Modules;
using Infrastructure.Factories.EffectsFactory;
using Infrastructure.Services.InputService;
using MVPBase;
using UnityEngine;

namespace Ball.Presenter
{
    public class BallPresenter : PresenterBase<BallModel, BallView>
    {
        private BallView _ballView;
        private BallModel _ballModel;

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

        public override void LinkPresenter(BallModel model, BallView view)
        {
            _ballView = view;
            _ballModel = model;

            _ballPhysics.Configure(
                linearDrag: model.LinearDrag,
                angularDrag: model.AngularDrag,
                squishFactor: model.SquishFactor,
                stretchFactor: model.StretchFactor
            );

            _inputProcessor.Configure(
                minDistanceToApplyForce: model.MinDistanceToApplyForce,
                distanceToForceCoefficient: model.DistanceToForceCoefficient,
                baseAppliedForce: model.BaseAppliedForce
            );

            view.Enabling += Enable;
            view.Disabling += Disable;

            Enable();
        }

        public sealed override void Enable()
        {
            _inputProcessor.Enable();

            _inputProcessor.ApplyingForceUpdated += OnApplyingForceUpdated;
            _inputProcessor.ForceCalculated += OnApplyingForceCalculated;

            TransformView.ObjectCollided += OnCollided;
            TransformView.VelocityUpdateRequested += OnVelocityUpdateRequested;
            TransformView.CurrentVelocityRequested += OnCurrentVelocityRequested;
            TransformView.SquishStarted += OnSquishStarted;
            TransformView.SquishFinished += OnSquishFinished;
        }

        public sealed override void Disable()
        {
            _inputProcessor.Disable();

            _inputProcessor.ApplyingForceUpdated -= OnApplyingForceUpdated;
            _inputProcessor.ForceCalculated -= OnApplyingForceCalculated;

            TransformView.ObjectCollided -= OnCollided;
            TransformView.VelocityUpdateRequested -= OnVelocityUpdateRequested;
            TransformView.CurrentVelocityRequested -= OnCurrentVelocityRequested;
            TransformView.SquishStarted -= OnSquishStarted;
            TransformView.SquishFinished -= OnSquishFinished;
        }

        private void OnSquishStarted() =>
            _ballPhysics.SetRotationActive(false);

        private void OnSquishFinished() =>
            _ballPhysics.SetRotationActive(true);

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

        private void ProceedHitEffects(Vector3 hitNormal)
        {
            Vector3 ballPosition = _ballView.TransformView.CurrentPosition;
            Quaternion hitRotation = Quaternion.LookRotation(hitNormal);
            
            _effectFactory.CreateEffect(EffectType.HitCloud, ballPosition, hitRotation);
        }

        private void ProceedSquishOnCollision(Vector3 collisionNormal)
        {
            Vector3 originalScale = _ballView.TransformView.OriginalScale;
            Vector3 squishScale = _ballPhysics.GetSquishScale(originalScale, collisionNormal);

            if (_ballPhysics.CurrentVelocity > _ballModel.MinVelocityToSquish)
                TransformView.Squish(squishScale, _ballModel.SquishDuration, _ballModel.SquishColor);
        }

        private void ProceedPhysicalHit(Collision collision) =>
            _ballPhysics.ProceedBallHit(collision);

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
        private BallForceView ForceView => _ballView.ForceView;
    }
}