using MVPBase;
using UnityEngine;

namespace Ball.Model
{
    public class BallModel : ModelBase
    {
        public float LinearDrag { get; private set; }
        public float AngularDrag { get; private set; }
        public float MinDistanceToApplyForce { get; private set; }
        public float DistanceToForceCoefficient { get; private set; }
        public float BaseAppliedForce { get; private set; }
        public float MinVelocityToSquish { get; private set; }
        public float MinDisplayedForceScale { get; private set; }
        public float MaxDisplayedForceScale { get; private set; }
        public float StretchFactor { get; private set; }
        public float SquishFactor { get; private set; }
        public float SquishDuration { get; private set; }
        public Color SquishColor { get; private set; }


        public void SetInputProcessingConfig(float minDistanceToApplyForce, float distanceToForceCoefficient,
            float baseAppliedForce)
        {
            MinDistanceToApplyForce = minDistanceToApplyForce;
            DistanceToForceCoefficient = distanceToForceCoefficient;
            BaseAppliedForce = baseAppliedForce;
        }

        public void SetForceScaleConfig(float minDisplayedForceScale, float maxDisplayedForceScale)
        {
            MinDisplayedForceScale = minDisplayedForceScale;
            MaxDisplayedForceScale = maxDisplayedForceScale;
        }

        public void SetSquishAnimationConfig(float squishDuration, Color squishColor)
        {
            SquishDuration = squishDuration;
            SquishColor = squishColor;
        }

        public void SetPhysicsConfig(float linearDrag, float angularDrag, float minVelocityToSquish, float squishFactor,
            float stretchFactor)
        {
            LinearDrag = linearDrag;
            AngularDrag = angularDrag;
            MinVelocityToSquish = minVelocityToSquish;
            SquishFactor = squishFactor;
            StretchFactor = stretchFactor;
        }
    }
}