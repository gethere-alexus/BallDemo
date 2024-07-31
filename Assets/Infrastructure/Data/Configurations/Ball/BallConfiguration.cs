using Infrastructure.Data.Configurations.Ball.Modules;
using UnityEngine;

namespace Infrastructure.Data.Configurations.Ball
{
    [CreateAssetMenu(menuName = "Configuration/BallConfiguration", fileName = "BallConfiguration")]
    public class BallConfiguration : ScriptableObject
    {
        [SerializeField] private BallPhysicsConfiguration _physicsConfiguration;
        [SerializeField] private BallInputProcessingConfiguration _inputProcessingConfiguration;
        [SerializeField] private BallSquishConfiguration _squishConfiguration;
        [SerializeField] private BallForceScaleConfiguration _forceScaleConfiguration;

        public BallForceScaleConfiguration ForceScaleConfiguration => _forceScaleConfiguration;
        public BallPhysicsConfiguration PhysicsConfiguration => _physicsConfiguration;

        public BallInputProcessingConfiguration InputProcessingConfiguration => _inputProcessingConfiguration;

        public BallSquishConfiguration SquishConfiguration => _squishConfiguration;
    }
}