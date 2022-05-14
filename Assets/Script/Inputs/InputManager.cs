using UnityEngine;

namespace Excappalitas.Inputs {
    public class InputManager : MonoBehaviour {
        PlayerControls inputAsset;

        [HideInInspector] public Vector2 MouseCoordinates;
        [HideInInspector] public Vector2 MovementInput;

        [HideInInspector] public Excappalitas.Inputs.InputComponents HoldInput;
        [HideInInspector] public Excappalitas.Inputs.InputComponents UnresetHoldInput;

        public void ResetInteractionInput() {
            HoldInput.Value = 0.0f;
            UnresetHoldInput.Value = 0.0f;        
        }

        private void OnEnable() {

            HoldInput = new InputComponents("HoldInput");
            UnresetHoldInput = new InputComponents("UnresetHoldInput");

            if (inputAsset == null) {
                
                inputAsset = new PlayerControls();

                EnableHoldInput();
                EnableUnresetHoldInput();
                EnablePlayerMovementInput();
                EnableMouseMovement();

            }

            inputAsset.Enable();
        }

        private void OnDisable() { inputAsset.Disable(); }

        private void EnableHoldInput() {
            inputAsset.PlayerInteraction.Discrete.started += _ => HoldInput.HasInput = true;
            inputAsset.PlayerInteraction.Discrete.performed += _ => HoldInput.HasInput = true;
            inputAsset.PlayerInteraction.Discrete.canceled += _ => HoldInput.HasInput = false;
        }

        private void EnableUnresetHoldInput() {
            inputAsset.PlayerInteraction.Discrete.started += _ => UnresetHoldInput.HasInput = true;
            inputAsset.PlayerInteraction.Discrete.performed += _ => UnresetHoldInput.HasInput = true;
            inputAsset.PlayerInteraction.Discrete.canceled += _ => UnresetHoldInput.HasInput = false;
        }

        private void EnablePlayerMovementInput() {
            inputAsset.PlayerMovement.Movement.performed += inputActions
                => MovementInput = inputActions.ReadValue<Vector2>();
            inputAsset.PlayerMovement.Movement.canceled += _
                => MovementInput = new Vector2(0.0f, 0.0f);
        }

        private void EnableMouseMovement() {
            inputAsset.PlayerMovement.MouseMovement.performed += inputAction
                => MouseCoordinates = Camera.main.ScreenToWorldPoint (inputAction.ReadValue<Vector2>());
        }

        public void CalculateHoldInput() {
            if (!HoldInput.HasInput) {
                HoldInput.Value = 0.0f;
                return;
            }

            HoldInput.Value += UnityEngine.Time.deltaTime;
        }

        public void CalculateHoldInputUnreseted() {
            UnresetHoldInput.HasInput = HoldInput.HasInput;
            if (UnresetHoldInput.HasInput) UnresetHoldInput.Value += UnityEngine.Time.deltaTime;
        }
    }
}