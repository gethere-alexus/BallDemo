using CodeBase.Sources.Modules.MapBase;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.MapFactory
{
    public interface IMapFactory
    {
        Map CreateMap(string mapID, Vector3 at);
    }
}