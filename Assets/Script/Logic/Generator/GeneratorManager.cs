using UnityEngine;
using Excappalitas.Interactable;

namespace Excappalitas.Logic.Generator {
    public class GeneratorManager : MonoBehaviour {
        public static GeneratorManager Instance { get; private set; }

        public GeneratorData[] Generators { get; private set; }

        private int generatorActivatedCounter;

        private void OnEnable() {
            if (Instance == null) Instance = this;
            Generators = GetAllGenerator();
        }

        public bool IsAllGeneratorActivatedUpdated() {
            UpdateGeneratorStatus();
            return IsAllGeneratorActivated();
        }

        private bool IsAllGeneratorActivated() => generatorActivatedCounter >= Generators.Length;

        private void UpdateGeneratorStatus() {
            generatorActivatedCounter = 0;
            foreach (var generator in Generators) {
                if (generator.IsActivated) generatorActivatedCounter++;
            }
        }

        private GeneratorData[] GetAllGenerator() {
            return GameObject.FindObjectsOfType<GeneratorData>();
        }
    }
}