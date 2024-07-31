using MapBase;
using UnityEngine;

namespace Infrastructure.Factories.MapFactory
{
    public interface IMapFactory
    {
        Map CreateMap(string mapID, Vector3 at);
    }
}