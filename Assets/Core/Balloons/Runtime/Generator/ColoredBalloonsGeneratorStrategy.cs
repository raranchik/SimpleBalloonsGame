using System.Collections;
using Core.Balloons.Field;
using Core.Balloons.Kinds;
using Core.Balloons.Kinds.Colored;
using Core.Balloons.Move;
using Core.Balloons.Presets;
using Core.Balloons.Presets.Colored;
using Core.Base.Map;
using Core.Base.Pool;
using Core.Base.Random;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Generator
{
    public class ColoredBalloonsGeneratorStrategy : IBalloonsGeneratorStrategy, IPostInitializable
    {
        private const float MinWait = 0.5f;
        private const float MaxWait = 0.75f;

        [Inject] private readonly RandomDecorator m_Random;
        [Inject] private readonly IPool<IColoredBalloon> m_Pool;
        [Inject] private readonly BaseMap<GameObject, IColoredBalloon> m_Colored;
        [Inject] private readonly BaseMap<GameObject, IBalloon> m_AliveMap;
        [Inject] private readonly BaseMap<string, IBalloonPreset> m_PresetMap;
        [Inject] private readonly BaseMap<IBalloon, BalloonSinMoveArgs> m_SinMoveArgs;
        [Inject] private readonly BalloonsFieldHelper m_FieldHelper;
        private IColoredBalloonPreset m_Preset;

        public string Id => BalloonsDefinition.ColoredBalloonId;

        public void PostInitialize()
        {
            m_Preset = m_PresetMap.GetValue(Id) as IColoredBalloonPreset;
        }

        public IEnumerator GenerateBalloon()
        {
            var balloon = m_Pool.Pop();
            var colors = m_Preset.Colors;
            var colorId = m_Random.Range(0, colors.Length - 1);
            balloon.SetColor(colors[colorId]);
            var generatePos = m_FieldHelper.GetBalloonRandomInstantiatePosition();
            balloon.MoveToPosition(generatePos);
            var frequency = m_Random.Range(BalloonsSinMoveDefinition.MinFrequency,
                BalloonsSinMoveDefinition.MaxFrequency);
            var amplitude = m_Random.Range(BalloonsSinMoveDefinition.MinAmplitude,
                BalloonsSinMoveDefinition.MaxAmplitude);
            var speed = m_Random.Range(m_Preset.MinSpeed, m_Preset.MaxSpeed);
            var moveArgs = new BalloonSinMoveArgs()
            {
                Balloon = balloon,
                Frequency = frequency,
                Amplitude = amplitude,
                Speed = speed,
                StartX = generatePos.x,
            };
            balloon.SetActive(true);

            m_Colored.Add(balloon.GameObject, balloon);
            m_AliveMap.Add(balloon.GameObject, balloon);
            m_SinMoveArgs.Add(balloon, moveArgs);

            var waitForSeconds = m_Random.Range(MinWait, MaxWait);
            yield return new WaitForSeconds(waitForSeconds);
        }

        public void DisposeBalloon(IBalloon balloon)
        {
            if (!m_Colored.Contains(balloon.GameObject))
            {
                return;
            }

            var colored = m_Colored.GetValue(balloon.GameObject);
            DisposeBalloon(colored);
        }

        public void DisposeBalloon(IColoredBalloon balloon)
        {
            balloon.SetActive(false);
            m_Colored.Remove(balloon.GameObject);
            m_AliveMap.Remove(balloon.GameObject);
            m_SinMoveArgs.Remove(balloon);
            m_Pool.Push(balloon);
        }
    }
}