using Infrastructure.Data.GameConfiguration.Ball.Modules;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.Data.GameConfiguration.Ball
{
    [CreateAssetMenu(menuName = "Configuration/BallConfiguration", fileName = "BallConfiguration")]
    public class BallConfiguration : ScriptableObject
    {
        [FormerlySerializedAs("_physicsConfiguration")] [SerializeField] private BallPhysicsConfig _physicsConfig;
        [FormerlySerializedAs("_inputProcessingConfiguration")] [SerializeField] private BallInputProcessingConfig _inputProcessingConfig;
        [FormerlySerializedAs("_squishConfig")] [FormerlySerializedAs("_squishConfiguration")] [SerializeField] private BallSquishAnimationConfig _squishAnimationConfig;
        [FormerlySerializedAs("_forceScaleConfiguration")] [SerializeField] private BallForceScaleConfig _forceScaleConfig;

        public BallForceScaleConfig ForceScaleConfig => _forceScaleConfig;
        public BallPhysicsConfig PhysicsConfig => _physicsConfig;

        public BallInputProcessingConfig InputProcessingConfig => _inputProcessingConfig;

        public BallSquishAnimationConfig SquishAnimationConfig => _squishAnimationConfig;
    }
}