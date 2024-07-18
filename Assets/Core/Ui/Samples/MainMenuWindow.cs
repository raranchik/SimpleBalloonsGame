using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Ui.Samples
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Button m_PlayButton;
        [SerializeField] private Button m_NewPlayerButton;
        [SerializeField] private Button m_OpenLeaderboardButton;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetOnPlayGame(Action action)
        {
            m_PlayButton.onClick.RemoveAllListeners();
            m_PlayButton.onClick.AddListener(action.Invoke);
        }

        public void SetOnOpenCreateNewPlayer(Action action)
        {
            m_NewPlayerButton.onClick.RemoveAllListeners();
            m_NewPlayerButton.onClick.AddListener(action.Invoke);
        }

        public void SetOnOpenLeaderboard(Action action)
        {
            m_OpenLeaderboardButton.onClick.RemoveAllListeners();
            m_OpenLeaderboardButton.onClick.AddListener(action.Invoke);
        }
    }
}