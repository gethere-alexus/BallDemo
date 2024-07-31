using Camera;
using Infrastructure.Services.GameBuilder;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SceneContext
{
    public class CameraInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private float _followSpeed;
        
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _cameraRotation;
        
        [SerializeField] private UnityEngine.Camera _mainCamera;
        
        public override void InstallBindings() => 
            Container.BindInterfacesTo<CameraInstaller>().FromInstance(this).AsSingle();

        public void Initialize()
        {
            IGameBuilder gameBuilder = Container.Resolve<IGameBuilder>();
            CameraFollower cameraFollower = _mainCamera.gameObject.AddComponent<CameraFollower>();

            cameraFollower.Configure(followSpeed: _followSpeed, offset: _positionOffset, rotation: _cameraRotation);
            cameraFollower.SetTrack(gameBuilder.BallInstance.transform);
        }
    }
}