using CodeBase.Sources.Modules.Ball.Presenter;
using CodeBase.Sources.Modules.Ball.View.ForceView;
using UnityEngine;
using Zenject;

namespace CodeBase.Sources.Modules.Ball.View
{
    public class BallView : MonoBehaviour
    {
        private BallForceView _ballForceView;


        [Inject]
        public void Construct(BallForceView ballForceView, BallPresenter ballPresenter)
        {
            _ballForceView = ballForceView;
            Presenter = ballPresenter;
        }

        private void OnEnable() => 
            Presenter.ApplyingForceChanged += UpdateView;

        private void OnDisable() => 
            Presenter.ApplyingForceChanged -= UpdateView;

        private void UpdateView()
        {
            UpdateForceView();
        }

        private void UpdateForceView()
        {
            float applyingForce = Presenter.ApplyingClearForce;
            float forceDirection = Presenter.ForceAngleDirection;
            
            _ballForceView.SetForceView(transform.position, applyingForce, forceDirection);
        }
        

        public BallPresenter Presenter { get; private set; }
    }
}