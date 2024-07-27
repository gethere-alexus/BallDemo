using CodeBase.Infrastructure.Factories.MapFactory;
using CodeBase.Infrastructure.Factories.MapFactory.BallFactory;
using CodeBase.Infrastructure.Factories.MapFactory.CoinFactory;
using CodeBase.Infrastructure.Services.GameBuilder;
using CodeBase.Sources.Modules.Ball.View.ForceView;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers.SceneContext
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