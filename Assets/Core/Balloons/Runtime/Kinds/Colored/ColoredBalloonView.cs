using UnityEngine;

namespace Core.Balloons.Kinds.Colored
{
    public class ColoredBalloonView : MonoBehaviour, IColoredBalloon
    {
        [SerializeField] private Transform m_Transform;
        [SerializeField] private SpriteRenderer m_Renderer;

        public bool IsActive => gameObject.activeSelf;
        public GameObject GameObject => gameObject;
        public Vector3 Position => m_Transform.position;

        public void MoveToPosition(Vector3 position)
        {
            m_Transform.position = position;
        }

        public void SetColor(Color color)
        {
            m_Renderer.color = color;
        }

        public void SetActive(bool isActive)
        {
            if (IsActive == isActive)
            {
                return;
            }

            gameObject.SetActive(isActive);
        }
    }
}