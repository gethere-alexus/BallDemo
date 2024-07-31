using UnityEngine;

namespace Infrastructure.Data.Configurations.Coin
{
    [CreateAssetMenu(menuName = "Configuration/CoinConfiguration", fileName = "CoinConfiguration")]
    public class CoinConfiguration : ScriptableObject
    {
        [SerializeField] private int _receivingCoins;

        public int ReceivingCoins => _receivingCoins;
    }
}