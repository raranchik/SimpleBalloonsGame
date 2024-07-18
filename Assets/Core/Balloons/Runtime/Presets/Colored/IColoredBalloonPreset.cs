using UnityEngine;

namespace Core.Balloons.Presets.Colored
{
    public interface IColoredBalloonPreset : IBalloonPreset
    {
        Color[] Colors { get; }
    }
}