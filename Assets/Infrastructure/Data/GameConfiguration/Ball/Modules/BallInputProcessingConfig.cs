using System;

namespace Infrastructure.Data.GameConfiguration.Ball.Modules
{
    [Serializable]
    public class BallInputProcessingConfig 
    {
        public float MinDistanceToForce;
        public float DistanceToForceCoefficient;
        public float BaseAppliedForce;
    }
}