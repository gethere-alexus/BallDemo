using System;
using APIs.Activatable;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.InputService;
using Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ball.Input
{
    public class BallInputProcessor : IActivatable
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


        public BallInputProcessor(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Configure(float minDistanceToApplyForce, float distanceToForceCoefficient, float baseAppliedForce)
        {
            _minDistanceToApplyForce = minDistanceToApplyForce;
            _distanceToForceCoefficient = distanceToForceCoefficient;
            _baseAppliedForce = baseAppliedForce;
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