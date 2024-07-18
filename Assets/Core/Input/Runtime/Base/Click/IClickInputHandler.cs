using UnityEngine;

namespace Core.Input.Base.Click
{
    public interface IClickInputHandler
    {
        void OnClick(Vector2 position);
    }
}