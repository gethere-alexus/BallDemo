using CodeBase.Infrastructure.Factories.MapFactory;
using CodeBase.Infrastructure.Factories.MapFactory.BallFactory;
using CodeBase.Infrastructure.Factories.MapFactory.CoinFactory;
using CodeBase.Infrastructure.Factories.UIFactory;
using CodeBase.Sources.Modules.Ball.Presenter;
using CodeBase.Sources.Modules.MapBase;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameBuilder
{
    public class GameBuilder : IGameBuilder
    {
        private readonly IMapFactory _mapFactory;
        private readonly ICoinFactory _coinFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IUIFactory _uiFactory;

        private const string DefaultMapID = "DefaultMap";
        private const string DefaultBallID = "Football";
        
        public GameBuilder(IMapFactory mapFactory, ICoinFactory coinFactory, IBallFactory ballFactory, IUIFactory uiFactory)
        {
            _mapFactory = mapFactory;
            _coinFactory = coinFactory;
            _ballFactory = ballFactory;
            _uiFactory = uiFactory;
        }

        public void Build(Vector3 buildPosition)
        {
            _uiFactory.CreateBalanceDisplay();
            
            MapInstance = _mapFactory.CreateMap(DefaultMapID, buildPosition);

            CreateCoins(MapInstance.CoinSpawnPoints, MapInstance.CoinsStorage);
            CreateBall(MapInstance.BallSpawnPoint);
        }

        private void CreateBall(Vector3 spawnPoint) => 
            BallInstance = _ballFactory.CreateBall(DefaultBallID, spawnPoint, Quaternion.identity);

        private void CreateCoins(Vector3[] spawnPoints, Transform storage)
        {
            foreach (var spawnPoint in spawnPoints)
                _coinFactory.CreateCoin(spawnPoint, storage);
        }

        public Map MapInstance { get; private set; }

        public BallPresenter BallInstance { get; private set; }
    }
}