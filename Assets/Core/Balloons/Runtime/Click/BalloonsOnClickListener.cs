using Core.Balloons.Kinds;
using Core.Base.Map;
using Core.Camera;
using Core.Input.Base.Click;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Click
{
    public class BalloonsOnClickListener : IInitializable, IClickInputListener 
    {
        [Inject] private readonly IClickInputNotifier m_ClickInputNotifier;
        [Inject] private readonly BalloonsClickBehavioursContext m_BehavioursContext;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;
        [Inject] private readonly CameraHelper m_CameraHelper;
        [Inject] private readonly BaseMap<GameObject, IBalloon> m_Alive;

        public void Initialize()
        {
            m_ClickInputNotifier.AddClickListener(this);
        }

        public void OnClick(Vector2 screenPosition)
        {
            var worldPos = m_CameraHelper.ScreenToWorldPoint(m_MainCameraMarker.Camera, screenPosition);
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider == null)
            {
                return;
            }

            var gameObject = hit.collider.gameObject;
            if (!m_Alive.Contains(gameObject))
            {
                return;
            }

            m_BehavioursContext.PassBalloon(gameObject);
        }
    }
}