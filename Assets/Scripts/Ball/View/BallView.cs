using System;
using Ball.View.ForceView;
using MVPBase;
using UnityEngine;
using Zenject;

namespace Ball.View
{
    public class BallView : ViewBase
    {
        [SerializeField] private BallTransformView _transformView;

        public event Action Enabling, Disabling;
        public BallForceView ForceView { get; private set; }
        public BallTransformView TransformView => _transformView;

        [Inject]
        public void Construct(BallForceView ballForceView) => 
            ForceView = ballForceView;

        private void OnEnable() => 
            Enabling?.Invoke();

        private void OnDisable() => 
            Disabling?.Invoke();
    }
}