using UnityEngine;

namespace Core.Balloons.Kinds.Colored
{
    public interface IColoredBalloon : IBalloon
    {
        void SetColor(Color color);
    }
}