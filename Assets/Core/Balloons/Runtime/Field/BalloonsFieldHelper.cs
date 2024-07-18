using Core.Base.Random;
using Core.Camera;
using UnityEngine;
using VContainer;

namespace Core.Balloons.Field
{
    public class BalloonsFieldHelper
    {
        [Inject] private readonly CameraHelper m_CameraHelper;
        [Inject] private readonly MainCameraMarker m_MainCameraMarker;
        [Inject] private readonly RandomDecorator m_Random;

        public Vector3 GetBalloonRandomInstantiatePosition()
        {
            var minViewportX = CameraDefinition.MinViewportX + BalloonsFieldDefinition.LeftRightOffset;
            var maxViewportX = CameraDefinition.MaxViewportX - BalloonsFieldDefinition.LeftRightOffset;
            var x = m_Random.Range(minViewportX, maxViewportX);
            var y = CameraDefinition.MinViewportY - BalloonsFieldDefinition.BottomOffset;
            var viewportPos = new Vector3(x, y);
            return m_CameraHelper.ViewportToWorldPoint(m_MainCameraMarker.Camera, viewportPos);
        }
    }
}