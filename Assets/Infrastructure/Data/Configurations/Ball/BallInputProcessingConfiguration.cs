using System;

namespace Infrastructure.Data.Configurations.Ball
{
    [Serializable]
    public class BallInputProcessingConfiguration 
    {
        public float MinDistanceToForce;
        public float DistanceToForceCoefficient;
        public float BaseAppliedForce;
    }
}