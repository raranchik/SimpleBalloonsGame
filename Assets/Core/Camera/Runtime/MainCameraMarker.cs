using UnityEngine;

namespace Core.Camera
{
    public class MainCameraMarker : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera m_Camera;

        public UnityEngine.Camera Camera => m_Camera;
    }
}