using UnityEngine;

namespace Core.Input.Base.Click
{
    public interface IClickInputListener
    {
        void OnClick(Vector2 screenPosition);
    }
}