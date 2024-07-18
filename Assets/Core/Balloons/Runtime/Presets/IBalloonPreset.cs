namespace Core.Balloons.Presets
{
    public interface IBalloonPreset
    {
        string Id { get; }
        float MinSpeed { get; }
        float MaxSpeed { get; }
        int Score { get; }
    }
}