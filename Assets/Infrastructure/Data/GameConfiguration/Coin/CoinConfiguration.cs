using UnityEngine;

namespace Infrastructure.Data.GameConfiguration.Coin
{
    [CreateAssetMenu(menuName = "Configuration/CoinConfiguration", fileName = "CoinConfiguration")]
    public class CoinConfiguration : ScriptableObject
    {
        [SerializeField] private int _receivingCoins;
        [SerializeField] private float _animationTimeToDisappear;
        [SerializeField] private float _rotationSpeed;
        
        public float RotationSpeed => _rotationSpeed;
        public float AnimationTimeToDisappear => _animationTimeToDisappear;
        public int ReceivingCoins => _receivingCoins;
    }
}