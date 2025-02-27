using System;
using APIs.Activatable;
using APIs.Configurable;
using Ball.Modules.Physics;
using Cysharp.Threading.Tasks;
using Infrastructure.Data.GameConfiguration.Ball.Modules;
using Infrastructure.Services.InputService;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ball.Modules.Input
{
    public class BallInputProcessor : IActivatable, IConfigurable<BallInputProcessingConfig>
    {
        private readonly IInputService _inputService;

        private bool _isForceTracked;

        private float _baseAppliedForce;
        private float _minDistanceToApplyForce;
        private float _distanceToForceCoefficient;

        private Vector2 _startPosition;
        private Vector2 _endPosition;

        private static readonly Vector2 ReferenceDirection = Vector2.up;

        public event Action ApplyingForceUpdated;
        public event Action ForceCalculated;

        public float ApplyingForce => _baseAppliedForce + ApplyingClearForce;
        public float ForceAngleDirection { get; private set; }
        public float ApplyingClearForce { get; private set; }


        public BallInputProcessor(IInputService inputService) => 
            _inputService = inputService;

        public void Configure(BallInputProcessingConfig inputProcessingConfig)
        {
            _minDistanceToApplyForce = inputProcessingConfig.MinDistanceToForce;
            _distanceToForceCoefficient = inputProcessingConfig.DistanceToForceCoefficient;
            _baseAppliedForce = inputProcessingConfig.BaseAppliedForce;
        }

        public void Enable()
        {
            InputModule.Ball.AddForce.started += OnAddForceStarted;
            InputModule.Ball.AddForce.canceled += OnAddForceFinished;
        }

        public void Disable()
        {
            InputModule.Ball.AddForce.started -= OnAddForceStarted;
            InputModule.Ball.AddForce.canceled -= OnAddForceFinished;
        }

        private void OnAddForceStarted(InputAction.CallbackContext context)
        {
            _startPosition = CurrentMousePosition;
            TrackForce(onTrackStopped: ResetApplyingForce);
        }

        private void OnAddForceFinished(InputAction.CallbackContext obj)
        {
            if (ApplyingClearForce > CustomPhysics.NoForce)
                ForceCalculated?.Invoke();
            SetTrackStatus(isActive : false);
        }

        private async void TrackForce(Action onTrackStopped)
        {
            SetTrackStatus(isActive: true);
            
            while (_isForceTracked)
            {
                await UniTask.NextFrame();
                UpdateApplyingForce();
            }
            
            onTrackStopped?.Invoke();
        }

        private void UpdateApplyingForce()
        {
            _endPosition = CurrentMousePosition;

            float angleToSet = Vector2.SignedAngle(ReferenceDirection, CurrentDirection);
            float forceToSet = CurrentDistance > _minDistanceToApplyForce ? 
                CurrentDistance * _distanceToForceCoefficient : CustomPhysics.NoForce;  
            
            SetForceAngleDirection(angleToSet);
            SetApplyingClearForce(forceToSet);
        }

        private void SetForceAngleDirection(float toSet)
        {
            ForceAngleDirection = toSet;
            ApplyingForceUpdated?.Invoke();
        }

        private void SetApplyingClearForce(float toSet)
        {
            ApplyingClearForce = toSet;
            ApplyingForceUpdated?.Invoke();
        }

        private void ResetApplyingForce() => 
            SetApplyingClearForce(CustomPhysics.NoForce);

        private void SetTrackStatus(bool isActive) => 
            _isForceTracked = isActive;

        private InputModule InputModule => _inputService.InputModule;
        private Vector2 CurrentDirection => _endPosition - _startPosition;
        private float CurrentDistance => Vector2.Distance(_startPosition, _endPosition);
        private Vector2 CurrentMousePosition => InputModule.Ball.Position.ReadValue<Vector2>();
    }
}