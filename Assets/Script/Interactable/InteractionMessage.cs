using UnityEngine;
using Excappalitas.Inputs;

namespace Excappalitas.Interactable {
    public class InteractionMessage {

        /// <sumary> Interactable ID </sumary>
        public int InteractableID { get { return interactableID; } }

        public Transform Caller { get; set; }

        private int interactableID;
        private InputComponents[] inputs;

        public InteractionMessage(int interactableID, Transform caller, params InputComponents[] inputs) {
            this.interactableID = interactableID;
            Caller = caller;

            if (inputs != null) this.inputs = inputs;
        }

        public InputComponents GetInputByName(string name) {
            foreach (var input in inputs) if (input.Name == name) return input;
            throw new System.Exception ("<InteractionMessage>: method GetInputByName : Input not found.");
        }
    }

}