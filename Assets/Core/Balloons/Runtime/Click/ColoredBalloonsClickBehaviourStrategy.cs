using Core.Balloons.Generator;
using Core.Balloons.Kinds;
using Core.Balloons.Kinds.Colored;
using Core.Balloons.Move;
using Core.Balloons.Presets;
using Core.Base.Map;
using Core.Base.Pool;
using Core.Score;
using UnityEngine;
using VContainer;

namespace Core.Balloons.Click
{
    public class ColoredBalloonsClickBehaviourStrategy : IBalloonsClickBehaviorStrategy
    {
        [Inject] private readonly ScoreController m_ScoreController;
        [Inject] private readonly IPool<IColoredBalloon> m_Pool;
        [Inject] private readonly ColoredBalloonsGeneratorStrategy m_GeneratorStrategy;
        [Inject] private readonly BaseMap<string, IBalloonPreset> m_BalloonPresetsMap;
        [Inject] private readonly BaseMap<GameObject, IColoredBalloon> m_ColoredMap;
        [Inject] private readonly BaseMap<GameObject, IBalloon> m_AliveMap;
        [Inject] private readonly BaseMap<IBalloon, BalloonSinMoveArgs> m_MoveArgsMap;

        public void PassBalloon(GameObject gameObject)
        {
            if (!m_ColoredMap.Contains(gameObject))
            {
                return;
            }

            var preset = m_BalloonPresetsMap.GetValue(BalloonsDefinition.ColoredBalloonId);
            m_ScoreController.AddScore(preset.Score);
            var balloon = m_ColoredMap.GetValue(gameObject);
            m_GeneratorStrategy.DisposeBalloon(balloon);
        }
    }
}