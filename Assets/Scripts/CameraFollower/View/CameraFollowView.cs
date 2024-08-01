using System;
using MVPBase;
using UnityEngine;

namespace CameraFollower.View
{
    public class CameraFollowView : ViewBase
    {
        public event Action TrackUpdateRequested;
        public event Action Enabling, Disabling, Destroying;
        
        public Vector3 CurrentPosition => transform.position;

        public void SetPosition(Vector3 toSet) => 
            transform.position = toSet;
        private void OnEnable() => 
            Enabling?.Invoke();

        private void FixedUpdate() => 
            TrackUpdateRequested?.Invoke();

        private void OnDisable() => 
            Disabling?.Invoke();

        private void OnDestroy() => 
            Destroying?.Invoke();
    }
}