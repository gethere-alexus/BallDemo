using Infrastructure.Data.GameConfiguration.UI.CoinDisplay.Modules;
using UnityEngine;

namespace Infrastructure.Data.GameConfiguration.UI.CoinDisplay
{
    [CreateAssetMenu(menuName = "Configuration/CoinDisplay", fileName = "CoinDisplayConfiguration")]
    public class CoinDisplayConfiguration : ScriptableObject
    {
        [SerializeField] private CoinDisplayAnimationConfig _animationConfig;
        [SerializeField] private CoinDisplayAddConfig _addConfig;

        public CoinDisplayAddConfig AddConfig => _addConfig;
        public CoinDisplayAnimationConfig AnimationConfig => _animationConfig;
    }
}