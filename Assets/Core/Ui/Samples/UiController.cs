using Core.Balloons.Generator;
using Core.Base.Factory;
using Core.Base.Map;
using Core.Player;
using Core.Score;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Ui.Samples
{
    public class UiController : IPostInitializable
    {
        [Inject] private readonly BalloonsGeneratorContext m_GeneratorContext;
        [Inject] private readonly PlayersController m_PlayersController;
        [Inject] private readonly PlayerScorePanelView m_CurrentPlayerPanel;
        [Inject] private readonly BestPlayerScorePanelView m_BestPlayerPanel;
        [Inject] private readonly CreateNewPlayerPanel m_CreateNewPlayerPanel;
        [Inject] private readonly LeaderboardWindow m_LeaderboardWindow;
        [Inject] private readonly ScoreController m_ScoreController;
        [Inject] private readonly ExitMainMenuButton m_ExitMainMenuButton;
        [Inject] private readonly GameScorePanelView m_GameScorePanel;
        [Inject] private readonly MainMenuWindow m_MainMenuWindow;
        private readonly PlayerScorePanelView m_Prefab;
        private readonly BaseMap<string, PlayerScorePanelView> m_PanelMap = new PlayerScorePanelsMap();

        public UiController(PlayerScorePanelView prefab)
        {
            m_Prefab = prefab;
        }

        public void PostInitialize()
        {
            m_MainMenuWindow.SetOnPlayGame(PlayGame);
            m_MainMenuWindow.SetOnOpenCreateNewPlayer(OpenCreateNewPlayer);
            m_MainMenuWindow.SetOnOpenLeaderboard(OpenLeaderboard);

            m_CreateNewPlayerPanel.SetOnApply(CreateNewPlayer);
            m_CreateNewPlayerPanel.SetOnCancel(CloseCreateNewPlayer);

            m_LeaderboardWindow.SetOnClose(CloseLeaderboard);

            m_ExitMainMenuButton.SetOnExit(StopGame);

            UpdateCurrent();
            UpdateAll();
        }

        private void PlayGame()
        {
            if (m_PlayersController.GetCurrent() == null)
            {
                OpenCreateNewPlayer();
                return;
            }

            CloseMainMenu();
            CloseLeaderboard();
            CloseCreateNewPlayer();
            m_GameScorePanel.SetScore(0.ToString());
            m_GameScorePanel.SetActive(true);
            m_GeneratorContext.StartGenerate();
        }

        private void StopGame()
        {
            m_GeneratorContext.StopGenerate();
            var current = m_PlayersController.GetCurrent();
            if (current.BestScore < m_ScoreController.Score)
            {
                current.BestScore = m_ScoreController.Score;
            }

            m_ScoreController.ResetScore();
            m_PlayersController.UpdateCurrent(current);
            m_PlayersController.UpdateAll(m_PlayersController.GetAll());
            UpdateCurrent();
            UpdateAll();
            m_GameScorePanel.SetActive(false);
            m_PlayersController.Save();
            OpenMainMenu();
        }

        private void CreateNewPlayer()
        {
            var name = m_CreateNewPlayerPanel.GetInput();
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (m_PlayersController.Exist(name))
            {
                return;
            }

            var data = new PlayerData()
            {
                Name = name,
                BestScore = 0,
            };
            m_PlayersController.UpdateCurrent(data);
            m_PlayersController.UpdateAll(m_PlayersController.GetAll());
            m_PlayersController.Save();
            UpdateCurrent();
            UpdateAll();
            CloseCreateNewPlayer();
        }

        private void UpdatePlayerScorePanel(PlayerScorePanelView scorePanel, PlayerData data)
        {
            scorePanel.SetName(data.Name);
            scorePanel.SetScore(data.BestScore.ToString());
        }

        private void OpenMainMenu()
        {
            m_MainMenuWindow.SetActive(true);
        }

        private void CloseMainMenu()
        {
            m_MainMenuWindow.SetActive(false);
        }

        private void CloseCreateNewPlayer()
        {
            m_CreateNewPlayerPanel.SetActive(false);
        }

        private void OpenCreateNewPlayer()
        {
            m_CreateNewPlayerPanel.ResetInput();
            m_CreateNewPlayerPanel.SetActive(true);
        }

        private void CloseLeaderboard()
        {
            m_LeaderboardWindow.SetActive(false);
        }

        private void OpenLeaderboard()
        {
            m_LeaderboardWindow.SetActive(true);
        }

        private void UpdateCurrent()
        {
            var current = m_PlayersController.GetCurrent();
            if (current == null)
            {
                m_CurrentPlayerPanel.SetName(PlayerDefinition.PlayerUnknownName);
                m_CurrentPlayerPanel.SetScore(0.ToString());
                return;
            }

            UpdatePlayerScorePanel(m_CurrentPlayerPanel, current);
        }

        private void UpdateAll()
        {
            var all = m_PlayersController.GetAll();
            if (all.Count <= 0)
            {
                m_BestPlayerPanel.SetName(PlayerDefinition.PlayerUnknownName);
                m_BestPlayerPanel.SetScore(0.ToString());
                m_LeaderboardWindow.SetEmptyPlaceholderIsActive(true);
                return;
            }

            all.Sort((a, b) => a.BestScore.CompareTo(b.BestScore));
            for (int i = all.Count - 1, j = 0; i >= 0; i--, j++)
            {
                var data = all[i];
                PlayerScorePanelView panel;
                if (!m_PanelMap.Contains(data.Name))
                {
                    panel = Object.Instantiate(m_Prefab, Vector3.zero, Quaternion.identity,
                        m_LeaderboardWindow.PanelsParent);
                    m_PanelMap.Add(data.Name, panel);
                }
                else
                {
                    panel = m_PanelMap.GetValue(data.Name);
                }

                panel.SetSiblingIndex(j);
                UpdatePlayerScorePanel(panel, data);
            }

            m_LeaderboardWindow.SetEmptyPlaceholderIsActive(false);
            UpdatePlayerScorePanel(m_BestPlayerPanel, all[^1]);
        }
    }
}