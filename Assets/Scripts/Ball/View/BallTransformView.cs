using System;
using System.Collections;
using UnityEngine;

namespace Ball.View
{
    public class BallTransformView : MonoBehaviour
    {
        private Coroutine _squishAnimation;
        
        private Material _ballMaterial;

        private Color _originalColor;

        private Vector3 _currentVelocity, _currentAngularVelocity;

        public event Action<Collision> ObjectCollided;
        
        public event Action CurrentVelocityRequested;
        public event Action VelocityUpdateRequested;

        public event Action SquishStarted, SquishFinished;
        public Vector3 CurrentPosition => transform.position;
        public Vector3 CurrentScale => transform.localScale;

        public Vector3 OriginalScale { get; private set; }

        public void SetAngularVelocity(Vector3 toSet) =>
            _currentAngularVelocity = toSet;

        public void SetVelocity(Vector3 toSet) =>
            _currentVelocity = toSet;

        public void Squish(Vector3 squishScale, float squishDuration, Color squishColor)
        {
            if(_squishAnimation != null)
                StopCoroutine(_squishAnimation);
            _squishAnimation = StartCoroutine(AnimateBallSquish(squishScale, squishDuration, squishColor));
        }

        private void Awake()
        {
            _ballMaterial = GetComponent<MeshRenderer>().material;
            
            OriginalScale = transform.localScale;
            _originalColor = _ballMaterial.color;
        }

        private void FixedUpdate()
        {
            CurrentVelocityRequested?.Invoke();
            
            if (TransformUpdatesRequired)
            {
                if (VelocityUpdatesRequired)
                    Move();

                if (AngularVelocityUpdatesRequired)
                    Rotate();
                
                VelocityUpdateRequested?.Invoke();
            }
        }

        private void Rotate() =>
            transform.Rotate(_currentAngularVelocity, Space.World);

        private void Move() => 
            transform.position += _currentVelocity;

        private IEnumerator AnimateBallSquish(Vector3 scaleToSet, float animationDuration, Color squishColor)
        {
            const int animationSteps = 2;

            SquishStarted?.Invoke();
            
            float stepDuration = animationDuration / animationSteps;
            float pastTime = 0;

            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _ballMaterial.color = Color.Lerp(_originalColor, squishColor, pastTime / stepDuration);
                transform.localScale = Vector3.Lerp(OriginalScale, scaleToSet, pastTime / stepDuration);
            }


            pastTime = 0;
            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _ballMaterial.color = Color.Lerp(squishColor, _originalColor, pastTime / stepDuration);
                transform.localScale = Vector3.Lerp(scaleToSet, OriginalScale, pastTime / stepDuration);
            }

            transform.localScale = OriginalScale;
            _ballMaterial.color = _originalColor;
            
            SquishFinished?.Invoke();
        }

        private void OnCollisionEnter(Collision collision) => 
            ObjectCollided?.Invoke(collision);

        private bool VelocityUpdatesRequired => _currentVelocity != Vector3.zero;
        private bool AngularVelocityUpdatesRequired => _currentAngularVelocity != Vector3.zero;
        private bool TransformUpdatesRequired => VelocityUpdatesRequired || AngularVelocityUpdatesRequired;
    }
}