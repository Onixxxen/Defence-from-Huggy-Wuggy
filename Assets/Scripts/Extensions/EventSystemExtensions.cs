using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Extensions
{
    static class EventSystemExtensions
    {
        public static T GetFirstComponentUnderPoint<T>(this EventSystem system, PointerEventData eventData) where T : class
        {
            var result = new List<RaycastResult>();
            system.RaycastAll(eventData, result);

            foreach (var reycast in result)
                if (reycast.gameObject.TryGetComponent<T>(out T component))
                    return component;

            return null;
        }
    }
}
