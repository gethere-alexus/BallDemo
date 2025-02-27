using System.Collections.Generic;

namespace Zenject
{
    public class ListPool<T> : StaticMemoryPool<List<T>>
    {
        public ListPool()
        {
            OnDespawnedMethod = OnDespawned;
        }

        public static ListPool<T> Instance { get; } = new ListPool<T>();

        void OnDespawned(List<T> list)
        {
            list.Clear();
        }
    }
}
