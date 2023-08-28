using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectLumina.UI
{
    public class UIEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent onPointerEnter, onPointerExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke();
        }
    }
}