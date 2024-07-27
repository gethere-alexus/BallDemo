using UnityEngine;

namespace CodeBase.Infrastructure.Data.Configurations.Ball
{
    [CreateAssetMenu(menuName = "Configuration/BallConfiguration", fileName = "BallConfiguration")]
    public class BallConfiguration : ScriptableObject
    {
        [SerializeField] private float _linearDrag = 0.02f;
        [SerializeField] private float _angularDrag = 0.02f;
        
        [SerializeField] private float _squishFactor = 0.25f;
        [SerializeField] private float _squishDuration = 0.3f;
        [SerializeField] private float _minSquishVelocity = 0.1f;
        [SerializeField] private float _stretchFactor = 1.5f;
        
        [SerializeField] private Color _squishColor = new(1, 0.4f, 0.4f, 1);

        public float LinearDrag => _linearDrag;
        public float AngularDrag => _angularDrag;
        public float SquishFactor => _squishFactor;
        public float SquishDuration => _squishDuration;
        public float MinSquishVelocity => _minSquishVelocity;
        public float StretchFactor => _stretchFactor;
        
        public Color SquishColor => _squishColor;
    }
}