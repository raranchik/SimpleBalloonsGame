using VContainer;

namespace Core.Score
{
    public class ScoreController
    {
        [Inject] private readonly GameScorePanelView m_View;

        private int m_Score;

        public int Score => m_Score;

        public void AddScore(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            m_Score += value;
            m_View.SetScore(m_Score.ToString());
        }

        public void ResetScore()
        {
            m_Score = 0;
        }
    }
}