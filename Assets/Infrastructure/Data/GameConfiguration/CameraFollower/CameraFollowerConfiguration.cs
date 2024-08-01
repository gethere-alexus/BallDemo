using UnityEngine;

namespace Infrastructure.Data.GameConfiguration.CameraFollower
{
    [CreateAssetMenu(menuName = "Configuration/CameraFollower", fileName = "CameraFollowerConfiguration")]
    public class CameraFollowerConfiguration : ScriptableObject
    {
        [SerializeField] private float _followSpeed;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _cameraRotation;
        
        public float FollowSpeed => _followSpeed;
        public Vector3 PositionOffset => _positionOffset;
        public Vector3 RotationOffset => _cameraRotation;
    }
}