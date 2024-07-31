using Ball.Model;
using Ball.Presenter;
using Ball.View;
using Infrastructure.Data.Configurations;
using Infrastructure.Data.Configurations.Ball;
using Infrastructure.Data.Configurations.Ball.Modules;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.Services.ConfigurationProvider.API;
using Infrastructure.StaticData;
using UnityEngine;
using Utils.Extensions;
using Zenject;
using IPrefabProvider = Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace Infrastructure.Factories.BallFactory
{
    public class BallFactory : IBallFactory, IConfigReader
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
            BallInputProcessingConfiguration inputProcessing =
                _ballConfiguration.InputProcessingConfiguration;

            BallPhysicsConfiguration ballPhysics =
                _ballConfiguration.PhysicsConfiguration;

            BallSquishConfiguration ballSquish =
                _ballConfiguration.SquishConfiguration;

            BallForceScaleConfiguration forceScale =
                _ballConfiguration.ForceScaleConfiguration;


            BallModel model = new BallModel()
                .With(model => model.SetPhysicsConfiguration(
                    linearDrag: ballPhysics.LinearDrag,
                    angularDrag: ballPhysics.AngularDrag))
                .With(model => model.SetForceScaleConfiguration(
                    minDisplayedForceScale: forceScale.MinimumDisplayedForceScale,
                    maxDisplayedForceScale: forceScale.MaximumDisplayedForceScale))
                .With(model => model.SetInputProcessingConfiguration(
                    minDistanceToApplyForce: inputProcessing.MinDistanceToForce,
                    distanceToForceCoefficient: inputProcessing.DistanceToForceCoefficient,
                    baseAppliedForce: inputProcessing.BaseAppliedForce))
                .With(model => model.SetSquishConfiguration(
                    minVelocityToSquish: ballSquish.MinVelocityToSquish,
                    squishDuration: ballSquish.SquishDuration,
                    squishColor: ballSquish.SquishColor,
                    squishFactor: ballSquish.SquishFactor,
                    stretchFactor: ballSquish.StretchFactor));

            return model;
        }

        public BallView CreateBall(string ballID, Vector3 at, Quaternion rotation)
        {
            string loadFrom = $"{PrefabPaths.BallStorage}{ballID}";

            BallModel ballModel = ConstructModel();

            BallView ballView =
                _prefabProvider.InstantiateWithContainer<BallView>(_diContainer, loadFrom, at, rotation);

            BallPresenter ballPresenter = _diContainer.Instantiate<BallPresenter>()
                .With(presenter => presenter.LinkPresenter(model: ballModel, view: ballView));

            return ballView;
        }
    }
}