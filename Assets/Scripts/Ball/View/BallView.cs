using System;
using Ball.View.Modules;
using MVPBase;
using UnityEngine;
using Zenject;

namespace Ball.View
{
    [RequireComponent(typeof(BallTransformView))]
    public class BallView : ViewBase
    {
        public event Action Enabling, Disabling;
        public BallForceView ForceView { get; private set; }
        public BallTransformView TransformView { get; private set; }

        [Inject]
        public void Construct(BallForceView ballForceView) => 
            ForceView = ballForceView;

        private void Awake()
        {
            TransformView = GetComponent<BallTransformView>();
        }

        private void OnEnable() => 
            Enabling?.Invoke();

        private void OnDisable() => 
            Disabling?.Invoke();
    }
}