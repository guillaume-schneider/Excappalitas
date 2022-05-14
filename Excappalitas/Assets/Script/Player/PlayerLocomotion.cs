using UnityEngine;
using Utils;
using Excappalitas.Inputs;

namespace Excappalitas.Player {
    public class PlayerLocomotion : MonoBehaviour
    {
        private InputManager inputs;
        private Rigidbody2D playerRigidbody;
        private PlayerStats stats;
        private Vector3 mousePosition;

        void Start () {
            inputs = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody2D>();
            stats = GetComponent<PlayerStats>();
        }

        private void Update () {
            DoMove();
            DoRotation();
        }

        private void DoMove() {
            playerRigidbody.velocity = new Vector2(stats.MoveSpeed * inputs.MovementInput.x,
                                              stats.MoveSpeed * inputs.MovementInput.y);

            if(Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
                Tools.FlipSprite(playerRigidbody, transform);
        }

        private void DoRotation() {
            mousePosition = inputs.MouseCoordinates;

            transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                mousePosition - transform.position
            );
        }
        
    }
}
