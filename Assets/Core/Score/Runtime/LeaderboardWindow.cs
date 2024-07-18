using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Score
{
    public class LeaderboardWindow : MonoBehaviour
    {
        [SerializeField] private Transform m_PanelsParent;
        [SerializeField] private GameObject m_EmptyPlaceholder;
        [SerializeField] private Button m_CloseButton;

        public Transform PanelsParent => m_PanelsParent;

        public void SetEmptyPlaceholderIsActive(bool isActive)
        {
            m_EmptyPlaceholder.SetActive(isActive);
        }

        public void SetOnClose(Action action)
        {
            m_CloseButton.onClick.RemoveAllListeners();
            m_CloseButton.onClick.AddListener(action.Invoke);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}