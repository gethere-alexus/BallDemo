using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.PrefabProvider
{
    public interface IPrefabProvider
    {
        TObject Instantiate<TObject>(TObject prefab, Vector3 at, Quaternion rotation) where TObject : Object;
        TObject Instantiate<TObject>(string path, Vector3 at, Quaternion rotation) where TObject : Object;
        TObject Instantiate<TObject>(string path) where TObject : Object;
        TObject Instantiate<TObject>(string path, Transform parent) where TObject : Object;
        TObject InstantiateWithZenject<TObject>(string path, Vector3 at, Quaternion rotation) where TObject : Object;
        TObject InstantiateWithZenject<TObject>(string path, Transform parent) where TObject : Object;
        TObject InstantiateWithContainer<TObject>(DiContainer container, string path, Vector3 at, Quaternion rotation) where TObject : Object;
    }
}