using UnityEngine;

namespace Core.Balloons.Presets.Colored
{
    public abstract class ColoredBalloonPresetBase : BalloonPresetBase, IColoredBalloonPreset
    {
        public abstract Color[] Colors { get; }
    }
}