using UnityEngine;

namespace Coin.View
{
    public class CoinRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, _rotationSpeed, 0));
        }
    }
}