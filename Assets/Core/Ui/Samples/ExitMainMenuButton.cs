using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Ui.Samples
{
    public class ExitMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button m_Button;

        public void SetOnExit(Action action)
        {
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(action.Invoke);
        }
    }
}