using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Ui.Samples
{
    public class CreateNewPlayerPanel : MonoBehaviour
    {
        [SerializeField] private Button m_ApplyButton;
        [SerializeField] private Button m_CancelButton;
        [SerializeField] private TMP_InputField m_InputField;

        public void SetOnApply(Action action)
        {
            m_ApplyButton.onClick.RemoveAllListeners();
            m_ApplyButton.onClick.AddListener(action.Invoke);
        }

        public void SetOnCancel(Action action)
        {
            m_CancelButton.onClick.RemoveAllListeners();
            m_CancelButton.onClick.AddListener(action.Invoke);
        }

        public void ResetInput()
        {
            m_InputField.text = "";
        }

        public string GetInput()
        {
            return m_InputField.text;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}