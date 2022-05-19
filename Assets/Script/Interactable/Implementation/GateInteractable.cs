using UnityEngine;

namespace Excappalitas.Interactable {
    public class GateInteractable : Interactable {

        private bool generatorActivated;

        private void Update() => UpdateGeneratorStatus();

        public override void OnInteract(InteractionMessage message) {
            base.OnInteract(message);
            
        }

        private void UpdateGeneratorStatus() {
            GameObject.FindObjectOfType<Excappalitas.LevelLogic.Generator.GeneratorManager>();
        }
    }
}
