using Infrastructure.Factories.CameraFollowerFactory;
using Infrastructure.Services.GameBuilder;
using UnityEngine;
using Utils.Extensions;
using Zenject;

namespace Infrastructure.Installers.SceneContext
{
    public class CameraInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Camera _mainCamera;
        
        public override void InstallBindings() => 
            Container.BindInterfacesTo<CameraInstaller>().FromInstance(this).AsSingle();

        public void Initialize()
        {
            IGameBuilder gameBuilder = Container.Resolve<IGameBuilder>();
            ICameraFollowerFactory followerFactory = Container.Resolve<ICameraFollowerFactory>();

            Transform target = gameBuilder.BallInstance.transform;
            
            followerFactory.CreateCameraFollower(_mainCamera)
                .With(follower => follower.SetTrack(target));
            
        }
    }
}