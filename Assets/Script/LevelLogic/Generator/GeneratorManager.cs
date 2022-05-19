using UnityEngine;
using Excappalitas.Interactable;

namespace Excappalitas.LevelLogic.Generator {
    public class GeneratorManager : MonoBehaviour {
        public Transform LastGeneratorActivated { get; private set; }
        public static GeneratorManager Instance { get; private set; }

        private GeneratorInteractable[] generators;
        private Transform[] generatorsTransform;

        private int generatorActivatedCounter;

        private void Start() {
            if (Instance == null) Instance = this;

            generators = GetAllGenerator();
            generatorsTransform = GetAllTransformGenerator(generators);
            
            EnableGeneratorStatusMonitoring();
        }

        public bool IsAllGeneratorActivated() => generators.Length >= generators.Length;

        private void EnableGeneratorStatusMonitoring() {
            GeneratorInteractable.OnGeneratorActivated += this.OnGeneratorActivated;
        }

        private void OnGeneratorActivated(Transform interactable, Transform interacter) {
            if (interactable != LastGeneratorActivated) generatorActivatedCounter++;
            LastGeneratorActivated = interactable;
        }

        private Transform[] GetAllTransformGenerator(GeneratorInteractable[] generators) {
            Transform[] generatorsTransform = new Transform[generators.Length];
            for (int i = 0; i < generators.Length; i++) {
                generatorsTransform[i] = generators[i].transform;
            }
            return generatorsTransform;
        }

        private GeneratorInteractable[] GetAllGenerator() {
            return GameObject.FindObjectsOfType<GeneratorInteractable>();
        }
    }
}