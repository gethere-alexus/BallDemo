using System;
using Ball.View.Modules.Transform.TransformModules;
using UnityEngine;

namespace Ball.View.Modules.Transform
{
    [RequireComponent(typeof(BallMovementView), typeof(BallRotationView),
        typeof(BallScaleView))]
    public class BallTransformView : MonoBehaviour
    {
        private BallMovementView _movementView;
        private BallRotationView _rotationView;
        public BallScaleView ScaleView { get; private set; }

        private Vector3 _currentVelocity, _currentAngularVelocity;

        public event Action CurrentVelocityRequested;
        public event Action VelocityUpdateRequested;
        
        public Vector3 CurrentPosition => transform.position;

        public void SetAngularVelocity(Vector3 toSet) =>
            _currentAngularVelocity = toSet;

        public void SetVelocity(Vector3 toSet) =>
            _currentVelocity = toSet;

        private void Awake()
        {
            ScaleView = GetComponent<BallScaleView>();
            
            _rotationView = GetComponent<BallRotationView>();
            _movementView = GetComponent<BallMovementView>();
        }

        private void FixedUpdate()
        {
            CurrentVelocityRequested?.Invoke();

            if (TransformUpdatesRequired)
            {
                if (VelocityUpdatesRequired)
                    _movementView.Move(_currentVelocity);

                if (AngularVelocityUpdatesRequired)
                    _rotationView.Rotate(_currentAngularVelocity);

                VelocityUpdateRequested?.Invoke();
            }
        }
        
        private bool VelocityUpdatesRequired => _currentVelocity != Vector3.zero;
        private bool AngularVelocityUpdatesRequired => _currentAngularVelocity != Vector3.zero;
        private bool TransformUpdatesRequired => VelocityUpdatesRequired || AngularVelocityUpdatesRequired;
    }
}