using UnityEngine;

namespace CodeBase.Infrastructure.Services.ResourceLoader
{
    public interface IResourceLoader
    {
        TLoad Load<TLoad>(string path) where TLoad : Object;
    }
}