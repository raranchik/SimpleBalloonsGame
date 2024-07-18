using TMPro;
using UnityEngine;

namespace Core.Score
{
    public class GameScorePanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Score;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetScore(string value)
        {
            m_Score.text = value;
        }
    }
}