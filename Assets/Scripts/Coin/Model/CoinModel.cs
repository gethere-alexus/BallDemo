using MVPBase;

namespace Coin.Model
{
    public class CoinModel : ModelBase
    {
        public int CoinsToClaim { get; private set; }
        public float AnimationTimeToDisappear { get; private set; }
        public float RotationSpeed { get; private set; }

        public void SetRotationSpeed(float rotationSpeed) => 
            RotationSpeed = rotationSpeed;
        
        public void SetClaimingCoins(int toSet) => 
            CoinsToClaim = toSet;

        public void SetAnimationConfiguration(float timeToDisappear) =>
            AnimationTimeToDisappear = timeToDisappear;
    }
}