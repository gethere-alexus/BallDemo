using UnityEngine;

namespace Infrastructure.Services.ResourceLoader
{
    public interface IResourceLoader
    {
        TLoad Load<TLoad>(string path) where TLoad : Object;
    }
}