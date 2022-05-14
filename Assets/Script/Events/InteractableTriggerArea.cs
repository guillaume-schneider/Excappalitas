using UnityEngine;
using Excappalitas.Interactable;
using Excappalitas.Inputs;

namespace Excappalitas.Events {
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractableTriggerArea : MonoBehaviour {

        public bool ResetOnExit;

        private Collider2D triggerCollider;

        private void Update() {
            if (triggerCollider == null) return;

            InputManager triggerInputController = triggerCollider.GetComponent<InputManager>();
            triggerInputController.CalculateHoldInput();
            triggerInputController.CalculateHoldInputUnreseted();
            InteractableEventManager.Update(
                new InteractionMessage(this.gameObject.GetInstanceID(), triggerCollider.transform,
                                       triggerInputController.HoldInput, triggerInputController.UnresetHoldInput)
            );        
        }

        private void OnTriggerEnter2D(Collider2D other) {
            triggerCollider = other;
        }

        private void OnTriggerExit2D(Collider2D other) {
            triggerCollider = null;
            if (ResetOnExit) other.GetComponent<InputManager>().ResetInteractionInput();
        }
    }
}