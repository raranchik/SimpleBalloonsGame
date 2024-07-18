using UnityEngine;

namespace Core.Base.Factory
{
    public class SimplePrefabFactory<T> : IFactory<T> where T : Component
    {
        private readonly T m_Prefab;

        public SimplePrefabFactory(T prefab)
        {
            m_Prefab = prefab;
        }

        public T Create()
        {
            var instance = Object.Instantiate(m_Prefab);
            return instance;
        }
    }
}