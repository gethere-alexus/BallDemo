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


        public void SetInputProcessingConfiguration(float minDistanceToApplyForce, float distanceToForceCoefficient,
            float baseAppliedForce)
        {
            MinDistanceToApplyForce = minDistanceToApplyForce;
            DistanceToForceCoefficient = distanceToForceCoefficient;
            BaseAppliedForce = baseAppliedForce;
        }

        public void SetForceScaleConfiguration(float minDisplayedForceScale, float maxDisplayedForceScale)
        {
            MinDisplayedForceScale = minDisplayedForceScale;
            MaxDisplayedForceScale = maxDisplayedForceScale;
        }

        public void SetSquishConfiguration(float minVelocityToSquish, float squishDuration, Color squishColor, float squishFactor, float stretchFactor)
        {
            MinVelocityToSquish = minVelocityToSquish;
            SquishDuration = squishDuration;
            SquishColor = squishColor;
            SquishFactor = squishFactor;
            StretchFactor = stretchFactor;
        }

        public void SetPhysicsConfiguration(float linearDrag, float angularDrag)
        {
            LinearDrag = linearDrag;
            AngularDrag = angularDrag;
        }
    }
}