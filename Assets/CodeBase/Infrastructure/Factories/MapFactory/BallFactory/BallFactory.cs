using CodeBase.Infrastructure.StaticData;
using CodeBase.Sources.Modules.Ball.Presenter;
using CodeBase.Sources.Modules.Ball.View;
using UnityEngine;
using Zenject;
using IPrefabProvider = CodeBase.Infrastructure.Services.PrefabProvider.IPrefabProvider;

namespace CodeBase.Infrastructure.Factories.MapFactory.BallFactory
{
    public class BallFactory : IBallFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IPrefabProvider _prefabProvider;

        public BallFactory(DiContainer diContainer, IPrefabProvider prefabProvider)
        {
            _diContainer = diContainer;
            _prefabProvider = prefabProvider;
        }

        public BallPresenter CreateBall(string ballID, Vector3 at, Quaternion rotation)
        {
            string loadFrom = $"{PrefabPaths.BallStorage}{ballID}";
            
            _diContainer.Bind<BallPresenter>().FromNewComponentSibling().AsTransient();
            
            BallView ballView = _prefabProvider.InstantiateWithContainer<BallView>(_diContainer, loadFrom, at, rotation);
            
            return ballView.Presenter;
        }
    }
}