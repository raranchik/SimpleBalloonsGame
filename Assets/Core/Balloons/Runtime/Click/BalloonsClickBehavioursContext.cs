using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Core.Balloons.Click
{
    public class BalloonsClickBehavioursContext
    {
        [Inject] private readonly IReadOnlyList<IBalloonsClickBehaviorStrategy> m_Strategies;

        public void PassBalloon(GameObject gameObject)
        {
            foreach (var strategy in m_Strategies)
            {
                strategy.PassBalloon(gameObject);
            }
        }
    }
}