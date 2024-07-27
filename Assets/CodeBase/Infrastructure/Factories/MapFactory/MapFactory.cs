using CodeBase.Infrastructure.Services.PrefabProvider;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Sources.Modules.MapBase;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.MapFactory
{
    public class MapFactory : IMapFactory
    {
        private readonly IPrefabProvider _prefabProvider;

        public MapFactory(IPrefabProvider prefabProvider) => 
            _prefabProvider = prefabProvider;

        public Map CreateMap(string mapID, Vector3 at)
        {
            string loadFrom = $"{PrefabPaths.MapStorage}{mapID}";
            Map mapInstance = _prefabProvider.Instantiate<Map>(loadFrom, at, Quaternion.identity);
            return mapInstance;
        }
        
    }
}