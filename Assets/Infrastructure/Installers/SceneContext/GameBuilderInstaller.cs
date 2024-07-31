using Ball.View.Modules;
using Infrastructure.Factories.BallFactory;
using Infrastructure.Factories.CoinFactory;
using Infrastructure.Factories.MapFactory;
using Infrastructure.Services.GameBuilder;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SceneContext
{
    public class GameBuilderInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private BallForceView _ballForceView;
        [SerializeField] private Transform _buildPoint;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {

            BindForceView();
            
            BindMapFactory();
            
            BindCoinFactory();
            
            BindBallFactory();
            
            BindGameBuilder();
            
            Container.BindInterfacesTo<GameBuilderInstaller>().FromInstance(this).AsSingle();
        }

        private void BindForceView() => 
            Container.BindInterfacesAndSelfTo<BallForceView>().FromComponentInNewPrefab(_ballForceView).AsSingle();

        private void BindMapFactory() => 
            Container.BindInterfacesTo<MapFactory>().AsSingle();

        private void BindCoinFactory() => 
            Container.BindInterfacesTo<CoinFactory>().AsSingle();
        
        private void BindBallFactory() => 
            Container.BindInterfacesTo<BallFactory>().AsSingle();
        
        private void BindGameBuilder() => 
            Container.BindInterfacesTo<GameBuilder>().AsSingle();

        public void Initialize()
        {
            IGameBuilder gameBuilder = Container.Resolve<IGameBuilder>();
            
            gameBuilder.Build(_buildPoint.position);
        }
    }
}