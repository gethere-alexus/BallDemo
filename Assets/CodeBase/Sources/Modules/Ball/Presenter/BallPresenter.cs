using System;
using CodeBase.Infrastructure.Factories.EffectsFactory;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Sources.Modules.Ball.Presenter.Physics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CodeBase.Sources.Modules.Ball.Presenter
{
    public class BallPresenter : MonoBehaviour
    {
        private IInputService _inputService;
        private IEffectFactory _effectFactory;
        
        private BallPhysics _ballPhysics;

        private Vector2 _startPosition, _endPosition;

        private bool _isForceTracked;

        private const float MinDistanceToForce = 100.0f;
        private const float DistanceToForceCoefficient = 0.01f;
        private const float BaseAppliedForce = 10.0f;

        private static readonly Vector2 ReferenceDirection = Vector2.up;

        public event Action ApplyingForceChanged;
        

        [Inject]
        public void Construct(IInputService inputService, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _inputService = inputService;
            _ballPhysics = GetComponentInChildren<BallPhysics>();
        }

        private void ApplyForce()
        {
            if (ApplyingClearForce > CustomPhysics.NoForce)
                _ballPhysics.AddForce(ApplyingForce, ForceAngleDirection);
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
            
            ForceAngleDirection = Vector2.SignedAngle(ReferenceDirection, CurrentDirection);
            ApplyingClearForce = CurrentDistance > MinDistanceToForce ? CurrentDistance * DistanceToForceCoefficient : CustomPhysics.NoForce;   
          
            ApplyingForceChanged?.Invoke();
        }

        private void OnAddForceStarted(InputAction.CallbackContext context)
        {
            _startPosition = CurrentMousePosition;
            TrackForce(onTrackStopped: ResetApplyingForce);
        }

        private void OnAddForceFinished(InputAction.CallbackContext obj)
        {
            ApplyForce();
            SetTrackStatus(isActive : false);
        }

        private void ResetApplyingForce()
        {
            ApplyingClearForce = CustomPhysics.NoForce;
            ApplyingForceChanged?.Invoke();
        }

        private void SetTrackStatus(bool isActive) => 
            _isForceTracked = isActive;

        private void OnEnable()
        {
            InputModule.Ball.AddForce.started += OnAddForceStarted;
            InputModule.Ball.AddForce.canceled += OnAddForceFinished;
        }

        private void OnDisable()
        {
            InputModule.Ball.AddForce.started -= OnAddForceStarted;
            InputModule.Ball.AddForce.canceled -= OnAddForceFinished;
        }

        private void OnCollisionEnter(Collision other)
        {
            Vector3 ballPosition = transform.position;
            Vector3 hitNormal = other.contacts[0].normal;

            Quaternion hitRotation = Quaternion.LookRotation(hitNormal);
            
            _effectFactory.CreateEffect(EffectType.HitCloud, ballPosition, hitRotation);
        }

        private InputModule InputModule => _inputService.InputModule;
        
        private Vector2 CurrentDirection => _endPosition - _startPosition;
        private Vector2 CurrentMousePosition => InputModule.Ball.Position.ReadValue<Vector2>();
        private float CurrentDistance => Vector2.Distance(_startPosition, _endPosition);
        
        public float ApplyingForce => BaseAppliedForce + ApplyingClearForce;
        public float ApplyingClearForce { get; private set; }

        public float ForceAngleDirection { get; private set; }
    }
}