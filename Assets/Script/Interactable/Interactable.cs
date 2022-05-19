using UnityEngine;
using Excappalitas.Events;

namespace Excappalitas.Interactable {
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        private void OnEnable() {
            InteractableEventManager.OnUpdate += OnInteract;
        }

        private void OnDisable() {
            InteractableEventManager.OnUpdate -= OnInteract;
        }

        public virtual void OnInteract(InteractionMessage message) {
            if (!IsThisObjectID(message.InteractableID)) return;
        }

        private bool IsThisObjectID(int objectID) 
            => objectID == this.gameObject.GetInstanceID();
    }
}