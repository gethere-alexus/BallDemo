using System;
using UnityEngine;

namespace Infrastructure.Data.Configurations.Ball.Modules
{
    [Serializable]
    public class BallSquishConfiguration
    {
        public float SquishFactor;
        public float SquishDuration;
        public float MinVelocityToSquish;
        public float StretchFactor;
        
        public Color SquishColor;
    }
}