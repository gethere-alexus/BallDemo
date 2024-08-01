using System;
using APIs.CoroutineRunner;
using Ball.View.Modules.Collision;
using Ball.View.Modules.Force;
using Ball.View.Modules.Material;
using Ball.View.Modules.Transform;
using MVPBase;
using UnityEngine;
using Zenject;

namespace Ball.View
{
    [RequireComponent(typeof(BallTransformView), typeof(BallCollisionView), 
        typeof(BallMaterialView))]
    public class BallView : ViewBase, ICoroutineRunner
    {
        public event Action Enabling, Disabling;
        public BallForceView ForceView { get; private set; }
        public BallTransformView TransformView { get; private set; }
        public BallCollisionView CollisionView { get; private set; }
        public BallMaterialView MaterialView { get; private set; }

        [Inject]
        public void Construct(BallForceView ballForceView) => 
            ForceView = ballForceView;

        private void Awake()
        {
            TransformView = GetComponent<BallTransformView>();
            CollisionView = GetComponent<BallCollisionView>();
            MaterialView = GetComponent<BallMaterialView>();
        }

        private void OnEnable() => 
            Enabling?.Invoke();

        private void OnDisable() => 
            Disabling?.Invoke();
    }
}