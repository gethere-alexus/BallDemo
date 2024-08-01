using MVPBase;

namespace UI.CoinDisplay.Model
{
    public class CoinBalanceModel : ModelBase
    {
        public float PunchScaleCoefficient;
        public float PunchTime;
        public float TimeToAddCoin;
        public int PunchStep;

        public void SetAnimationConfig(float punchScaleCoefficient, float punchTime)
        {
            PunchScaleCoefficient = punchScaleCoefficient;
            PunchTime = punchTime;
        }

        public void SetAddConfig(float timeToAddCoin, int punchStep)
        {
            TimeToAddCoin = timeToAddCoin;
            PunchStep = punchStep;
        }
    }
}