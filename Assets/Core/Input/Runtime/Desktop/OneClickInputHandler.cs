using Core.Input.Base;
using Core.Input.Base.Click;
using UnityEngine;
using VContainer;

namespace Core.Input.Desktop
{
    public class OneClickInputHandler : IInputHandler, IClickInputHandler
    {
        private const float InputThresholdMin = 6f;
        private const float InputThresholdMinSqr = InputThresholdMin * InputThresholdMin;
        private const float InputThresholdMax = 24f;
        private const float InputThresholdMaxSqr = InputThresholdMax * InputThresholdMax;
        private const int DefaultButton = 0;

        [Inject] private readonly UiInputHelper m_UiHelper;
        [Inject] private readonly ClickInputNotifier m_Notifier;
        private Vector2 m_StartPos;
        private bool m_IsClicked;

        public void Run()
        {
            Vector2 clickPos = UnityEngine.Input.mousePosition;
            if (IsClickDown() && !m_UiHelper.IsClickOnUi(clickPos))
            {
                m_IsClicked = true;
                m_StartPos = clickPos;
                return;
            }

            if (!IsClickUp() || !m_IsClicked)
            {
                return;
            }

            m_IsClicked = false;
            var sqrMagnitude = (clickPos - m_StartPos).sqrMagnitude;
            if (sqrMagnitude > InputThresholdMinSqr || sqrMagnitude > InputThresholdMaxSqr)
            {
                return;
            }

            OnClick(m_StartPos);
        }

        private bool IsClickDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(DefaultButton);
        }

        private bool IsClickUp()
        {
            return UnityEngine.Input.GetMouseButtonUp(DefaultButton);
        }

        public void OnClick(Vector2 position)
        {
            m_Notifier.NotifyOnClick(position);
        }
    }
}