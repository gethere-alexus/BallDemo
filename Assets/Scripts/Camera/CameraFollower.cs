using UnityEngine;

namespace Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraFollower : MonoBehaviour
    {
        private UnityEngine.Camera _mainCamera;

        private float _followSpeed;
        private Vector3 _positionOffset;
        private Quaternion _cameraRotation;

        private Transform _currentTrack;

        public void Configure(float followSpeed, Vector3 offset, Vector3 rotation)
        {
            _followSpeed = followSpeed;
            _positionOffset = offset;
            
            _mainCamera.transform.rotation = Quaternion.Euler(rotation);
        }

        public void SetTrack(Transform toTrack) => 
            _currentTrack = toTrack;

        private void DoSmoothMove(Vector3 target) => 
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime  / _followSpeed);

        private void FixedUpdate()
        {
            if(IsMovementRequired)
                DoSmoothMove(TrackPosition);
        }

        private void Awake() => 
            _mainCamera = gameObject.GetComponent<UnityEngine.Camera>();

        private bool IsMovementRequired => CurrentPosition != TrackPosition;
        private bool HasTrack => _currentTrack != null;
        private Vector3 TrackPosition => HasTrack ? _currentTrack.position + _positionOffset : transform.position;
        private Vector3 CurrentPosition => transform.position;
    }
}