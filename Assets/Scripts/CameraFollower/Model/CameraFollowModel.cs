using MVPBase;
using UnityEngine;

namespace CameraFollower.Model
{
    public class CameraFollowModel : ModelBase
    {
        public float FollowSpeed { get; private set; }
        public Vector3 PositionOffset { get; private set; }
        public Quaternion CameraRotation { get; private set; }

        public void SetFollowConfiguration(float followSpeed, Vector3 positionOffset, Quaternion rotationOffset)
        {
            FollowSpeed = followSpeed;
            PositionOffset = positionOffset;
            CameraRotation = rotationOffset;
        }
    }
}