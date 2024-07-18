using System.Collections.Generic;
using UnityEngine;

namespace Core.Input.Base.Click
{
    public class ClickInputNotifier : IClickInputNotifier
    {
        private readonly HashSet<IClickInputListener> m_Listeners = new HashSet<IClickInputListener>();

        public void AddClickListener(IClickInputListener listener)
        {
            m_Listeners?.Add(listener);
        }

        public void RemoveClickListener(IClickInputListener listener)
        {
            m_Listeners?.Remove(listener);
        }

        public void NotifyOnClick(Vector2 position)
        {
            foreach (var l in m_Listeners)
            {
                l.OnClick(position);
            }
        }
    }
}