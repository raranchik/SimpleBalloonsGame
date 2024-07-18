using UnityEngine;

namespace Core.Balloons.Presets.Colored
{
    [CreateAssetMenu(fileName = "NewColoredBalloonPreset", menuName = "Game/Create ColoredBalloonPreset", order = 0)]
    public class ColoredBalloonPreset : ColoredBalloonPresetBase
    {
        [SerializeField] private string m_Id;
        [SerializeField] private Vector2 m_Speed;
        [SerializeField] private int m_Score;
        [SerializeField] private Color[] m_Colors;

        public override string Id => m_Id;
        public override float MinSpeed => m_Speed.x;
        public override float MaxSpeed => m_Speed.y;
        public override int Score => m_Score;
        public override Color[] Colors => m_Colors;
    }
}