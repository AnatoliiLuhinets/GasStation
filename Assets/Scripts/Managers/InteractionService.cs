using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Managers
{
    public class InteractionService : MonoBehaviour
    {
        private List<InteractableObject> interactables = new List<InteractableObject>();

        private void Awake()
        {
            InteractableObject[] objects = FindObjectsOfType<InteractableObject>();
            foreach (var obj in objects)
            {
                RegisterInteractable(obj);
            }
        }

        public void RegisterInteractable(InteractableObject interactable)
        {
            if (!interactables.Contains(interactable))
            {
                interactables.Add(interactable);
                interactable.OnPointerDownPressed += HandlePointerDown;
                interactable.OnPointerUpPressed += HandlePointerUp;
            }
        }

        private void HandlePointerDown(InteractableObject interactableObject)
        {
            
        }

        private void HandlePointerUp(InteractableObject interactableObject)
        {
            
        }

        private void OnDestroy()
        {
            foreach (var interactable in interactables)
            {
                interactable.OnPointerDownPressed -= HandlePointerDown;
                interactable.OnPointerUpPressed -= HandlePointerUp;
            }
        }
    }
}