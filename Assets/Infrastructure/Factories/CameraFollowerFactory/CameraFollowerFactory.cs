using CameraFollower.Model;
using CameraFollower.Presenter;
using CameraFollower.View;
using Infrastructure.Data.Configurations;
using Infrastructure.Data.Configurations.CameraFollower;
using Infrastructure.Services.ConfigurationProvider;
using Infrastructure.Services.ConfigurationProvider.API;
using UnityEngine;
using Utils.Extensions;

namespace Infrastructure.Factories.CameraFollowerFactory
{
    public class CameraFollowerFactory : ICameraFollowerFactory, IConfigReader
    {
        private CameraFollowerConfiguration _followConfig;

        public CameraFollowerFactory(IConfigurationProvider configurationProvider)
        {
            configurationProvider.LoadConfiguration(this);
        }

        public void LoadConfiguration(GameConfiguration gameConfiguration) => 
            _followConfig = gameConfiguration.CameraFollowerConfiguration;

        public CameraFollowPresenter CreateCameraFollower(Camera attachTo)
        {
            CameraFollowModel model = new CameraFollowModel()
                .With(model => model.SetFollowConfiguration(
                    followSpeed: _followConfig.FollowSpeed,
                    positionOffset: _followConfig.PositionOffset,
                    rotationOffset: Quaternion.Euler(_followConfig.RotationOffset)));
            
            CameraFollowView view = attachTo.gameObject.AddComponent<CameraFollowView>();

            CameraFollowPresenter presenter = new CameraFollowPresenter()
                .With(presenter => presenter.LinkPresenter(model,view));

            return presenter;
        }
    }
}