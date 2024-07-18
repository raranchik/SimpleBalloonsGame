using UnityEngine;

namespace Core.Balloons.Click
{
    public interface IBalloonsClickBehaviorStrategy
    {
        void PassBalloon(GameObject gameObject);
    }
}