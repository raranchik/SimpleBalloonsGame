using System;
using Object = UnityEngine.Object;

namespace Core.Base.Factory
{
    public class SimplePrefabFactoryWithPostCreateAction<T> : IFactory<T> where T : Object
    {
        private readonly T m_Prefab;
        private readonly Action<T> m_OnCreate;

        public SimplePrefabFactoryWithPostCreateAction(T prefab, Action<T> onCreate)
        {
            m_Prefab = prefab;
            m_OnCreate = onCreate;
        }

        public T Create()
        {
            var instance = Object.Instantiate(m_Prefab);
            m_OnCreate?.Invoke(instance);
            return instance;
        }
    }
}