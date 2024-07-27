using System.Collections;
using CodeBase.Infrastructure.Data.Configurations;
using CodeBase.Infrastructure.Data.Configurations.Ball;
using CodeBase.Infrastructure.Services.ConfigurationProvider;
using CodeBase.Infrastructure.Services.ConfigurationProvider.API;
using UnityEngine;
using Zenject;

namespace CodeBase.Sources.Modules.Ball.Presenter.Physics
{
    public class BallPhysics : MonoBehaviour, IConfigReader
    {
        private float _linearDrag;
        private float _angularDrag;
        
        private float _squishFactor;
        private float _squishDuration;
        private float _minSquishVelocity;
        private float _stretchFactor;

        private Color _squishColor;
        private Color _originalColor;

        private Vector3 _originalScale;
        private Vector3 _currentVelocity;
        private Vector3 _currentAngularVelocity;

        private Material _ballMaterial;

        private bool _isRotationActive;

        private const float DirectionNormalizationDegree = 90;
        private const float ForceToVelocityCoefficient = 0.02f;
        private const float ForceToAngularVelocityCoefficient = 1.5f;

        [Inject]
        public void Construct(IConfigurationProvider configurationProvider) => 
            configurationProvider.LoadConfiguration(this);

        public void LoadConfiguration(GameConfiguration gameConfiguration)
        {
            BallConfiguration ballConfiguration = gameConfiguration.BallConfiguration;

            _linearDrag = ballConfiguration.LinearDrag;
            _angularDrag = ballConfiguration.AngularDrag;

            _squishFactor = ballConfiguration.SquishFactor;
            _squishDuration = ballConfiguration.SquishDuration;
            _minSquishVelocity = ballConfiguration.MinSquishVelocity;
            _stretchFactor = ballConfiguration.StretchFactor;

            _squishColor = ballConfiguration.SquishColor;
        }

        public void AddForce(float applyingForce, float forceDirection)
        {
            float moveDirection = (forceDirection + DirectionNormalizationDegree) * Mathf.Deg2Rad;

            Vector3 forceVector = new Vector3(Mathf.Cos(moveDirection), 0, Mathf.Sin(moveDirection));
            forceVector *= applyingForce * ForceToVelocityCoefficient;

            _currentVelocity += forceVector;

            Vector3 torque = Vector3.Cross(Vector3.up, forceVector) * ForceToAngularVelocityCoefficient;
            torque *= applyingForce * ForceToAngularVelocityCoefficient;

            _currentAngularVelocity += torque;
        }

        private void SetRotationActive(bool isActive) =>
            _isRotationActive = isActive;

        private void Awake()
        {
            _ballMaterial = GetComponent<MeshRenderer>().material;
            
            _originalScale = transform.localScale;
            _originalColor = _ballMaterial.color;
        }

        private void Start() => 
            SetRotationActive(true);

        private void FixedUpdate()
        {
            bool calculationsRequired = _currentVelocity != Vector3.zero || _currentAngularVelocity != Vector3.zero;

            if (calculationsRequired)
            {
                ApplyDragChanges();

                if (_currentVelocity != Vector3.zero)
                    MoveBall();

                if (_currentAngularVelocity != Vector3.zero && _isRotationActive)
                    RotateBall();
            }
        }

        private void ApplyDragChanges()
        {
            const float noChangesMultiplication = 1.0f;

            float linearVelocityDragCoefficient = noChangesMultiplication - _linearDrag;
            _currentVelocity *= linearVelocityDragCoefficient;

            float angularVelocityDragCoefficient = noChangesMultiplication - _angularDrag;
            _currentAngularVelocity *= angularVelocityDragCoefficient;
        }


        private void MoveBall() =>
            transform.position += _currentVelocity;

        private void RotateBall() =>
            transform.Rotate(_currentAngularVelocity, Space.World);

        private void OnCollisionEnter(Collision collision)
        {
            _currentVelocity = Vector3.Reflect(_currentVelocity, collision.contacts[0].normal);
            _currentAngularVelocity = -Vector3.Reflect(_currentAngularVelocity, collision.contacts[0].normal);
            
            if(CurrentVelocity > _minSquishVelocity)
                ProceedSquish(collision.contacts[0].normal);
        }

        private void ProceedSquish(Vector3 surfaceNormal)
        {
            Vector3 newScale = _originalScale;

            if (Mathf.Abs(surfaceNormal.x) > Mathf.Abs(surfaceNormal.z))
            {
                newScale.x *= _squishFactor;
                newScale.y *= _stretchFactor;
            }
            else
            {
                newScale.z *= _squishFactor;
                newScale.y *= _stretchFactor;
            }

            StopAllCoroutines();
            StartCoroutine(AnimateBallSquish(newScale, _squishDuration));
        }

        private IEnumerator AnimateBallSquish(Vector3 scaleToSet, float animationDuration)
        {
            const int animationSteps = 2;

            SetRotationActive(false);

            float stepDuration = animationDuration / animationSteps;
            float pastTime = 0;

            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _ballMaterial.color = Color.Lerp(_originalColor, _squishColor, pastTime / stepDuration);
                transform.localScale = Vector3.Lerp(_originalScale, scaleToSet, pastTime / stepDuration);
            }


            pastTime = 0;
            while (pastTime < stepDuration)
            {
                yield return null;
                pastTime += Time.deltaTime;

                _ballMaterial.color = Color.Lerp(_squishColor, _originalColor, pastTime / stepDuration);
                transform.localScale = Vector3.Lerp(scaleToSet, _originalScale, pastTime / stepDuration);
            }

            transform.localScale = _originalScale;
            _ballMaterial.color = _originalColor;
            
            SetRotationActive(true);
        }

        private float CurrentVelocity => Mathf.Abs(_currentVelocity.x) + Mathf.Abs(_currentVelocity.y) + Mathf.Abs(_currentVelocity.z);
    }
}