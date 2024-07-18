using System.Collections.Generic;

namespace Core.Base.Map
{
    public abstract class BaseMap<T, U>
    {
        private readonly Dictionary<T, U> m_Map = new Dictionary<T, U>();

        public void Add(T key, U value)
        {
            if (m_Map.ContainsKey(key))
            {
                return;
            }

            m_Map.Add(key, value);
        }

        public void Remove(T key)
        {
            m_Map.Remove(key);
        }

        public bool Contains(T key)
        {
            return m_Map.ContainsKey(key);
        }

        public U GetValue(T key)
        {
            if (m_Map.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public Dictionary<T, U>.ValueCollection GetValues()
        {
            return m_Map.Values;
        }

        public bool HasValues()
        {
            return m_Map.Count > 0;
        }
    }
}