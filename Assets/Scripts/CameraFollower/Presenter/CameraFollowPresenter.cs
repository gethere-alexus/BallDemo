
using System;
using CameraFollower.Model;
using CameraFollower.View;
using MVPBase;
using UnityEngine;

namespace CameraFollower.Presenter
{
    public class CameraFollowPresenter : PresenterBase<CameraFollowModel, CameraFollowView>, IDisposable
    {
        private CameraFollowModel _followModel;
        private CameraFollowView _followView;
        
        private Transform _currentTrack;
        
        public override void LinkPresenter(CameraFollowModel model, CameraFollowView view)
        {
            _followModel = model;
            _followView = view;

            ConfigureView();
            
            _followView.Enabling += Enable;
            _followView.Disabling += Disable;
            
            Enable();
        }

        private void ConfigureView()
        {
            _followView.transform.rotation = _followModel.CameraRotation;
        }

        public void SetTrack(Transform toTrack) => 
            _currentTrack = toTrack;

        public override void Enable()
        {
            _followView.TrackUpdateRequested += OnTrackUpdateRequested;
        }

        public override void Disable()
        {
            _followView.TrackUpdateRequested -= OnTrackUpdateRequested;
        }

        public void Dispose()
        {
            _followView.Enabling -= Enable;
            _followView.Disabling -= Disable;
        }

        private void OnTrackUpdateRequested()
        {
            if(IsMovementRequired)
                DoFrameMove(TrackPosition);
        }

        private void DoFrameMove(Vector3 trackPosition)
        {
            Vector3 frameMove = Vector3.Lerp(_followView.CurrentPosition, trackPosition, Time.deltaTime  / _followModel.FollowSpeed);
            
            _followView.SetPosition(frameMove);
        }

        private bool IsMovementRequired => _followView.CurrentPosition != TrackPosition;

        private bool HasTrack => _currentTrack != null;

        private Vector3 TrackPosition => HasTrack ? _currentTrack.position + _followModel.PositionOffset : _followView.CurrentPosition;
    }
}