using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Core.Input.Base
{
    public class UiInputHelper : IInitializable
    {
        private const string UiLayerName = "UI";
        private static readonly int ULayerId = LayerMask.NameToLayer(UiLayerName);

        private PointerEventData m_CacheEventData;
        private List<RaycastResult> m_CacheRaycastResults;

        public void Initialize()
        {
            m_CacheEventData = new PointerEventData(EventSystem.current);
            m_CacheRaycastResults = new List<RaycastResult>();
        }

        public bool IsClickOnUi(in Vector2 position)
        {
            m_CacheEventData.position = position;
            EventSystem.current.RaycastAll(m_CacheEventData, m_CacheRaycastResults);
            if (m_CacheRaycastResults.Count <= 0)
            {
                return false;
            }

            var result = m_CacheRaycastResults.Any(result => result.gameObject.layer == ULayerId);
            m_CacheEventData.Reset();
            m_CacheRaycastResults.Clear();

            return result;
        }
    }
}