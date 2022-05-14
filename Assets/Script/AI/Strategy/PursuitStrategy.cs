using UnityEngine;
using Utils;

namespace Excappalitas.AI.Strategy {
    public class PursuitStrategy : MonoBehaviour, Excappalitas.IStrategy
    {
        private Excappalitas.AI.AIStats _stats;
        private Rigidbody2D _rigidbody;

        private AIManager AIManager;
        private Rigidbody2D _targetRb;
        private Transform _currentTarget;

        private void Start () {
            AIManager = GetComponent<Excappalitas.AI.AIManager>();
            _stats = GetComponent<Excappalitas.AI.AIStats>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Execute () {
            _currentTarget = AIManager.GetCurrentTarget();
            _targetRb = _currentTarget.GetComponent<Rigidbody2D>();
            Vector3 vel = Pursuit (_currentTarget);

            _rigidbody.velocity += new Vector2 (vel.x, vel.y);
            
            Vector3 rotationDir = (_currentTarget.position - transform.position).normalized;
            transform.rotation = (vel != Vector3.zero) ? Quaternion.LookRotation (Vector3.forward, rotationDir)
            : transform.rotation;
        }

        private Vector3 Pursuit (Transform target) {

            Vector3 toEvader = target.position - transform.position;
            double relativeHeading = Vector2.Dot (transform.forward, target.forward);

            if ((Vector2.Dot (transform.forward, toEvader) > 0 &&
                relativeHeading < - .95)) return Seek (target.position);

            float targetSpeed = _targetRb.velocity.magnitude;
            double lookAheadTime = toEvader.magnitude / (_stats.MaxSpeed + targetSpeed);

            return (Seek (new Vector3 ((float)(target.position.x + targetSpeed * lookAheadTime),
                                       (float)(target.position.y + targetSpeed * lookAheadTime),
                                        0.0f)));
        }

        private Vector3 Seek (Vector3 target) {
            return AITools.Seek (transform.position, target, _rigidbody.velocity, _stats.MoveSpeed);
        }

    }

}
