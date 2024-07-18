using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Balloons.Kinds;
using Core.Base.CoroutineRunner;
using Core.Base.Map;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Generator
{
    public class BalloonsGeneratorContext : IInitializable
    {
        [Inject] private readonly IReadOnlyList<IBalloonsGeneratorStrategy> m_Strategies;
        [Inject] private readonly BaseMap<string, IBalloonsGeneratorStrategy> m_StrategiesMap;
        [Inject] private readonly ICoroutineRunner m_CoroutineRunner;
        [Inject] private readonly BaseMap<GameObject, IBalloon> m_Alive;

        private Coroutine m_GenerationRoutine;

        public void Initialize()
        {
            foreach (var strategy in m_Strategies)
            {
                m_StrategiesMap.Add(strategy.Id, strategy);
            }
        }

        public void StartGenerate()
        {
            m_GenerationRoutine = m_CoroutineRunner.StartCoroutine(GenerateBalloons());
        }

        public void StopGenerate()
        {
            m_CoroutineRunner.StopCoroutine(m_GenerationRoutine);
            DisposeAllBalloons();
        }

        public void DisposeBalloon(IBalloon balloon)
        {
            foreach (var strategy in m_Strategies)
            {
                strategy.DisposeBalloon(balloon);
            }
        }

        private IEnumerator GenerateBalloons()
        {
            while (true)
            {
                var strategy = m_StrategiesMap.GetValue(BalloonsDefinition.ColoredBalloonId);
                yield return strategy.GenerateBalloon();
            }
        }

        private void DisposeAllBalloons()
        {
            var allBalloons = m_Alive.GetValues().ToList();
            foreach (var balloon in allBalloons)
            {
                DisposeBalloon(balloon);
            }
        }
    }
}