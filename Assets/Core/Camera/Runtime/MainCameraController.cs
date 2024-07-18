using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Camera
{
    public class MainCameraController : IInitializable
    {
        [Inject] private readonly CameraHelper m_CameraHelper;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;

        public void Initialize()
        {
            var camera = m_MainCameraMarker.Camera;
            var newOrthographicSize = m_CameraHelper.CalculateOrthographicSize(camera.orthographicSize,
                new Vector2Int(1284, 2778), 1f, camera.aspect);
            camera.orthographicSize = newOrthographicSize;
        }
    }
}