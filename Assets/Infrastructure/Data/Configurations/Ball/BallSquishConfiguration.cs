using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.Data.Configurations.Ball
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