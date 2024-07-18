using UnityEngine;

namespace Core.Balloons.Presets
{
    public abstract class BalloonPresetBase : ScriptableObject, IBalloonPreset
    {
        public abstract string Id { get; }
        public abstract float MinSpeed { get; }
        public abstract float MaxSpeed { get; }
        public abstract int Score { get; }
    }
}