using Infrastructure.Services.PrefabProvider;
using Infrastructure.StaticData;
using MapBase;
using UnityEngine;

namespace Infrastructure.Factories.MapFactory
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