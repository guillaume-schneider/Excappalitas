using UnityEngine;
using Excappalitas.Interactable;

namespace Excappalitas.Logic.Generator {
    public class GeneratorData : MonoBehaviour {
        public GeneratorInteractable Events { get; private set; }
        public bool IsActivated { get; private set; }

        private void OnEnable() { 
            Events = GetComponent<GeneratorInteractable>();
            Events.OnGeneratorActivated += this.OnGeneratorActivated;
        }

        private void OnGeneratorActivated(Transform interactable, Transform interacter) => IsActivated = true;

    }
}