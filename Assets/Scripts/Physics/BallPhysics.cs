using UnityEngine;

namespace Physics
{
    public class BallPhysics
    {
        private float _linearDrag;
        private float _angularDrag;
        private float _squishFactor;
        private float _stretchFactor;
        
        private Vector3 _currentVelocity;
        private Vector3 _currentAngularVelocity;

        private bool _isRotationActive;

        private const float DirectionNormalizationDegree = 90;
        private const float ForceToVelocityCoefficient = 0.02f;
        private const float ForceToAngularVelocityCoefficient = 1.5f;

        public Vector3 LineVelocity => _currentVelocity;
        public Vector3 AngularVelocity => _isRotationActive ? _currentAngularVelocity : Vector3.zero;

        public BallPhysics()
        {
            SetRotationActive(true);
        }

        public void Configure(float linearDrag, float angularDrag, float squishFactor, float stretchFactor)
        {
            _linearDrag = linearDrag;
            _angularDrag = angularDrag;
            _squishFactor = squishFactor;
            _stretchFactor = stretchFactor;
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

        public void ApplyFrameVelocityChanges()
        {
            const float noChangesMultiplication = 1.0f;

            float linearVelocityDragCoefficient = noChangesMultiplication - _linearDrag;
            _currentVelocity *= linearVelocityDragCoefficient;

            float angularVelocityDragCoefficient = noChangesMultiplication - _angularDrag;
            _currentAngularVelocity *= angularVelocityDragCoefficient;
        }

        public void SetRotationActive(bool isActive) =>
            _isRotationActive = isActive;

        public void ProceedBallHit(Collision collision)
        {
            _currentVelocity = Vector3.Reflect(_currentVelocity, collision.contacts[0].normal);
            _currentAngularVelocity = -Vector3.Reflect(_currentAngularVelocity, collision.contacts[0].normal);
        }
        
        public Vector3 GetSquishScale(Vector3 currentScale,Vector3 collidedNormal)
        {
            Vector3 newScale = currentScale;

            if (Mathf.Abs(collidedNormal.x) > Mathf.Abs(collidedNormal.z))
            {
                newScale.x *= _squishFactor;
                newScale.y *= _stretchFactor;
            }
            else
            {
                newScale.z *= _squishFactor;
                newScale.y *= _stretchFactor;
            }

            return newScale;
        }

        public float CurrentVelocity => Mathf.Abs(_currentVelocity.x) + Mathf.Abs(_currentVelocity.y) + Mathf.Abs(_currentVelocity.z);
    }
}