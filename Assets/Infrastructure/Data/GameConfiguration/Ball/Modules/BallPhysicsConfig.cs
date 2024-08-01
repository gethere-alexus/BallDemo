using System;

namespace Infrastructure.Data.GameConfiguration.Ball.Modules
{
    [Serializable]
    public class BallPhysicsConfig
    {
        public float LinearDrag;
        public float AngularDrag;
        public float StretchFactor;
        public float MinVelocityToSquish;
        public float SquishFactor;
    }
}