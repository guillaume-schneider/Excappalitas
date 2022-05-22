using Excappalitas.Logic.Generator;
using Excappalitas.Animation;
using UnityEngine;

namespace Excappalitas.Interactable {
    public class GateInteractable : Interactable {

        private bool generatorActivated;
        private IAnimation gateAnimation;

        private void Start() => gateAnimation = GetComponent<IAnimation>();

        public override void OnInteract(InteractionMessage message) {
            base.OnInteract(message);
            if (IsGateOpen()) OpenGate();
        }

        private bool IsGateOpen() {
            return GeneratorManager.Instance.IsAllGeneratorActivatedUpdated();
        }

        private void OpenGate() {
            // gateAnimation.Play();
            // Debug.Log("Gate: Open");
        }
    }
}
