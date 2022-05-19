using Excappalitas.Events;
using UnityEngine;

namespace Excappalitas.Interactable {
    public class GeneratorInteractable : Interactable {

        /// <sumary> return interactable and the interacter </sumary>
        public static System.Action<Transform, Transform> OnFailedFill;

        /// <sumary> return interactable and the interacter </sumary>
        public static System.Action<Transform, Transform> OnGeneratorActivated;

        [SerializeField] private bool ResetOnRelease;

        [SerializeField] private float FillRateTimeReached;
        [SerializeField] private float FailedFillProbability;

        private bool failedFill = false;
        private bool generatorActivated = false;
        private float fillValue = 0.0f;

        public override void OnInteract(InteractionMessage message) {
            base.OnInteract(message);
            if (HasValueReachPourcentage(fillValue, 99)) {
                OnGeneratorFilled(message.Caller);
                return;
            }
            float inputValue = GetInputValue(message);
            fillValue = Clamp01ByFillRateTime(inputValue);
        }

        private void OnGeneratorFilled(Transform filler) {
            if (IsFailedFillOnce() && OnFailedFill != null) 
                OnFailedFill(this.transform, filler);
            if (IsGeneratorActivatedOnce() && OnGeneratorActivated != null) 
                OnGeneratorActivated(this.transform, filler);
        }

        private bool IsGeneratorActivatedOnce() {
            if (!generatorActivated)  {
                generatorActivated = true;
                return true;
            }
            return false;
        }

        private bool IsFailedFillOnce() {
            if (!failedFill) {
                failedFill = true;
                return IsFailedFill();
            }
            return false;
        }

        private bool IsFailedFill() 
            => Utils.Probability.MaxAsymmetricRepartition(3, 1, 20) >= FailedFillProbability;

        private float GetInputValue(InteractionMessage message) 
            => (!ResetOnRelease) ? message.GetInputByName("HoldInput").Value 
                                 : message.GetInputByName("UnresetHoldInput").Value;

        private float Clamp01ByFillRateTime(float value) 
            => Utils.Math.InverseInterpolation(0.0f, FillRateTimeReached, value);

        private bool HasValueReachPourcentage(float value, float pourcentage) 
            => value * 100 >= pourcentage;

    }
}