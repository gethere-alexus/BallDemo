using System;
using MVPBase;
using UnityEngine;

namespace Coin.View
{
    public class CoinView : ViewBase
    {
        [SerializeField] private GameObject _coinGameObject;

        private float _rotationSpeed;
        public event Action<Collider> TriggerEntered;
        public event Action Enabling, Disabling;

        public GameObject CoinGameObject => _coinGameObject;

        public void SetRotationSpeed(float rotationSpeed) => 
            _rotationSpeed = rotationSpeed;
        
        private void OnEnable() => 
            Enabling?.Invoke();

        private void FixedUpdate()
        {
            if(HasRotationSpeed)
                transform.Rotate(new Vector3(0, _rotationSpeed, 0));
        }

        private void OnDisable() => 
            Disabling?.Invoke();

        private void OnTriggerEnter(Collider collidedWith) =>
            TriggerEntered?.Invoke(collidedWith);

        private bool HasRotationSpeed => _rotationSpeed != 0;
    }
}