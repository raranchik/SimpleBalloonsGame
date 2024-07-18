using TMPro;
using UnityEngine;

namespace Core.Score
{
    public class PlayerScorePanelView : MonoBehaviour
    {
        [SerializeField] private Transform m_Transform;
        [SerializeField] private TextMeshProUGUI m_NameT;
        [SerializeField] private TextMeshProUGUI m_Score;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetName(string value)
        {
            m_NameT.text = value;
        }

        public void SetScore(string value)
        {
            m_Score.text = value;
        }

        public void SetParent(Transform parent)
        {
            m_Transform.SetParent(parent);
        }

        public void SetSiblingIndex(int index)
        {
            m_Transform.SetSiblingIndex(index);
        }
    }
}