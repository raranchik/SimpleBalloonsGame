using Core.Input.Base;
using Core.Input.Base.Click;
using UnityEngine;
using VContainer;

namespace Core.Input.Touch
{
    public class OneTouchInputHandler : IInputHandler, IClickInputHandler
    {
        private const float InputThresholdMin = 6f;
        private const float InputThresholdMinSqr = InputThresholdMin * InputThresholdMin;
        private const float InputThresholdMax = 24f;
        private const float InputThresholdMaxSqr = InputThresholdMax * InputThresholdMax;

        [Inject] private readonly UiInputHelper m_UiHelper;
        [Inject] private readonly ClickInputNotifier m_Notifier;
        private Vector2 m_StartPos;
        private bool m_IsClicked;

        public void Run()
        {
            var touchCount = UnityEngine.Input.touchCount;
            if (touchCount <= 0 || touchCount > 1)
            {
                return;
            }

            var touch = UnityEngine.Input.GetTouch(0);
            var touchPos = touch.position;
            if (touch.phase == TouchPhase.Began && !m_UiHelper.IsClickOnUi(touchPos))
            {
                m_IsClicked = true;
                m_StartPos = touchPos;
                return;
            }

            if (touch.phase != TouchPhase.Ended || !m_IsClicked)
            {
                return;
            }

            m_IsClicked = false;
            var sqrMagnitude = (touchPos - m_StartPos).sqrMagnitude;
            if (sqrMagnitude > InputThresholdMinSqr || sqrMagnitude > InputThresholdMaxSqr)
            {
                return;
            }

            OnClick(m_StartPos);
        }

        public void OnClick(Vector2 position)
        {
            m_Notifier.NotifyOnClick(position);
        }
    }
}