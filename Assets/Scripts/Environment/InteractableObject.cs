using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Environment
{
    public class InteractableObject : BaseComponent, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<InteractableObject> OnPointerDownPressed; 
        public event Action<InteractableObject> OnPointerUpPressed; 
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownPressed?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpPressed?.Invoke(this);
        }
    }
}
