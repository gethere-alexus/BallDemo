using Infrastructure.Services.ResourceLoader;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.PrefabProvider
{
    public class PrefabProvider : IPrefabProvider
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly DiContainer _container;

        public PrefabProvider(IResourceLoader resourceLoader, DiContainer container)
        {
            _resourceLoader = resourceLoader;
            _container = container;
        }

        public TObject Instantiate<TObject>(TObject prefab, Vector3 at, Quaternion rotation) where TObject : Object
        {
            TObject instance = Object.Instantiate(prefab, at, rotation);
            return instance;
        }

        public TObject Instantiate<TObject>(string path, Vector3 at, Quaternion rotation) where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);
            return Instantiate(load, at, rotation);
        }

        public TObject Instantiate<TObject>(string path) where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);
            
            TObject instance = Object.Instantiate(load);
            return instance;
        }

        public TObject Instantiate<TObject>(string path, Transform parent) where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);
            
            TObject instance = Object.Instantiate(load, parent);
            return instance;
        }

        public TObject InstantiateWithZenject<TObject>(string path, Vector3 at, Quaternion rotation)
            where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);

            TObject instance = _container.InstantiatePrefabForComponent<TObject>(load, at, rotation, null);
            return instance;
        }

        public TObject InstantiateWithZenject<TObject>(string path, Transform parent) where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);

            TObject instance = _container.InstantiatePrefabForComponent<TObject>(load, parent);
            return instance;
        }

        public TObject InstantiateWithContainer<TObject>(DiContainer container, string path, Vector3 at, Quaternion rotation) where TObject : Object
        {
            TObject load = _resourceLoader.Load<TObject>(path);

            TObject instance = container.InstantiatePrefabForComponent<TObject>(load, at, rotation, null);
            return instance;
        }
    }
}