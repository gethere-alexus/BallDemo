using UnityEngine;

namespace Infrastructure.Services.ResourceLoader
{
    public class ResourceLoader : IResourceLoader
    {
        public TLoad Load<TLoad>(string path) where TLoad : Object => 
            Resources.Load<TLoad>(path);
    }
}