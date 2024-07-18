using System;
using System.Collections.Generic;

namespace Core.Base.Pool
{
    public class SimplePool<T> : IPool<T>
    {
        private readonly Stack<T> m_Stack = new Stack<T>();
        private readonly int m_ExpandSize;
        private readonly Func<T> m_CreateCallback;

        public SimplePool(int initialSize, int expandSize, Func<T> createCallback)
        {
            if (expandSize <= 0)
            {
                expandSize = 1;
            }

            m_ExpandSize = expandSize;
            m_CreateCallback = createCallback;

            if (initialSize <= 0)
            {
                initialSize = 1;
            }

            Expand(initialSize);
        }

        public void Push(T value)
        {
            m_Stack.Push(value);
        }

        public T Pop()
        {
            if (m_Stack.Count <= 0)
            {
                Expand(m_ExpandSize);
            }

            return m_Stack.Pop();
        }

        private void Expand(int size)
        {
            for (var i = 0; i < size; i++)
            {
                m_Stack.Push(m_CreateCallback.Invoke());
            }
        }
    }
}