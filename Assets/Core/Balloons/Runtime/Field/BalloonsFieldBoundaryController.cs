using System.Collections.Generic;
using Core.Balloons.Generator;
using Core.Balloons.Kinds;
using Core.Base.Map;
using Core.Camera;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Field
{
    public class BalloonsFieldBoundaryController : IInitializable, ITickable
    {
        [Inject] private readonly BalloonsGeneratorContext m_GeneratorContext;
        [Inject] private readonly CameraHelper m_CameraHelper;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;
        [Inject] private readonly BaseMap<GameObject, IBalloon> m_Alive;
        private readonly List<IBalloon> m_CacheDisposedBalloons = new List<IBalloon>();
        private float m_MaxBoundaryY;

        public void Initialize()
        {
            var maxBoundaryPoint = m_CameraHelper.ViewportToWorldPoint(m_MainCameraMarker.Camera, new Vector3(1f, 1f));
            m_MaxBoundaryY = maxBoundaryPoint.y;
        }

        public void Tick()
        {
            if (!m_Alive.HasValues())
            {
                return;
            }

            foreach (var balloon in m_Alive.GetValues())
            {
                var pos = balloon.Position;
                if (pos.y > m_MaxBoundaryY)
                {
                    m_CacheDisposedBalloons.Add(balloon);
                }
            }

            if (m_CacheDisposedBalloons.Count <= 0)
            {
                return;
            }

            foreach (var balloon in m_CacheDisposedBalloons)
            {
                m_GeneratorContext.DisposeBalloon(balloon);
            }

            m_CacheDisposedBalloons.Clear();
        }
    }
}