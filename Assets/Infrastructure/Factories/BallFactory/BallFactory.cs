using APIs.GameConfigReader;
using Ball.Model;
using Ball.Presenter;
using Ball.View;
using Infrastructure.Data.GameConfiguration;
using Infrastructure.Data.GameConfiguration.Ball;
using Infrastructure.Data.GameConfiguration.Ball.Modules;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.StaticData;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using IPrefabProvider = Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace Infrastructure.Factories.BallFactory
{
    public class BallFactory : IBallFactory, IGameConfigReader
    {
        private readonly DiContainer _diContainer;
        private readonly IPrefabProvider _prefabProvider;

        private BallConfiguration _ballConfiguration;

        public BallFactory(DiContainer diContainer, IPrefabProvider prefabProvider,
            IConfigurationProvider configProvider)
        {
            _diContainer = diContainer;
            _prefabProvider = prefabProvider;

            configProvider.LoadConfiguration(this);
        }


        public void LoadConfiguration(GameConfiguration gameConfiguration) =>
            _ballConfiguration = gameConfiguration.BallConfiguration;

        private BallModel ConstructModel()
        {
            BallInputProcessingConfig inputProcessing =
                _ballConfiguration.InputProcessingConfig;

            BallPhysicsConfig ballPhysics =
                _ballConfiguration.PhysicsConfig;

            BallSquishAnimationConfig ballSquishAnimation =
                _ballConfiguration.SquishAnimationConfig;

            BallForceScaleConfig forceScale =
                _ballConfiguration.ForceScaleConfig;


            BallModel model = new BallModel()
                .With(model => model.SetPhysicsConfig(
                    linearDrag: ballPhysics.LinearDrag,
                    angularDrag: ballPhysics.AngularDrag,
                    minVelocityToSquish: ballPhysics.MinVelocityToSquish, 
                    squishFactor: ballPhysics.SquishFactor,
                    stretchFactor: ballPhysics.StretchFactor))
                .With(model => model.SetForceScaleConfig(
                    minDisplayedForceScale: forceScale.MinimumDisplayedForceScale,
                    maxDisplayedForceScale: forceScale.MaximumDisplayedForceScale))
                .With(model => model.SetInputProcessingConfig(
                    minDistanceToApplyForce: inputProcessing.MinDistanceToForce,
                    distanceToForceCoefficient: inputProcessing.DistanceToForceCoefficient,
                    baseAppliedForce: inputProcessing.BaseAppliedForce))
                .With(model => model.SetSquishAnimationConfig(
                    squishDuration: ballSquishAnimation.SquishDuration,
                    squishColor: ballSquishAnimation.SquishColor));

            return model;
        }

        public BallView CreateBall(string ballID, Vector3 at, Quaternion rotation)
        {
            string loadFrom = $"{PrefabPaths.BallStorage}{ballID}";

            BallModel ballModel = ConstructModel();

            BallView ballView =
                _prefabProvider.InstantiateWithContainer<BallView>(_diContainer, loadFrom, at, rotation);

            BallPresenter ballPresenter = _diContainer.Instantiate<BallPresenter>()
                .With(presenter => presenter.LinkPresenter(ballModel: ballModel, ballView: ballView));

            return ballView;
        }
    }
}