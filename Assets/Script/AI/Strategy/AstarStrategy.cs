using UnityEngine;
using Pathfinding;
using Utils;


namespace Excappalitas.AI.Strategy {
    public class AstarStrategy : MonoBehaviour, IStrategy {

        private Path _path;
        private AIStats _stats;
        private Seeker _seeker;
        private Rigidbody2D _rigidbody;
        private AIManager AIManager;

        private Transform _currentTarget;
        private int _indexWaypoint = 0;

        [SerializeField]
        private float SpeedFactor;

        [SerializeField]
        private float _nextWaypointDistance = 3;
        private bool _reachedEndOfPath = false;

        private void Start () {
            _stats = GetComponent<AIStats>();
            _seeker = GetComponent<Seeker>();
            _rigidbody = GetComponent<Rigidbody2D>();
            AIManager = GetComponent<AIManager>();
        }

        public void Execute () {
            _currentTarget = AIManager.GetCurrentTarget();
            StartCoroutine ("SearchPath", .4f);
            HandleMovement ();
        }

        private void SearchPath () {
            if (_currentTarget != null) {
                _seeker.StartPath (transform.position, _currentTarget.position, OnPathComplete);
            }
        }

        private void HandleMovement() {        

            if (true) { // TODO: en fonction du temps
                if (_path == null) return;
                if (_path.vectorPath.Count == _indexWaypoint) _indexWaypoint = 0;

                _indexWaypoint++;

                Vector3 dir = (_path.vectorPath[_indexWaypoint] - transform.position).normalized;
                Vector3 velocity = dir * _stats.MoveSpeed * SpeedFactor;

                _rigidbody.velocity = new Vector2 (velocity.x, velocity.y);
                transform.rotation = Quaternion.LookRotation (
                    transform.forward,
                    dir
                );
            }
        }

        private void ClampPath () {
            if (_path == null) return;

            float distanceToWaypoint;
            while (true) {

                distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_indexWaypoint]);
                if (distanceToWaypoint < _nextWaypointDistance) {

                    if (_indexWaypoint + 1 < _path.vectorPath.Count) {
                        _indexWaypoint++;
                    } else {
                        break;
                    }

                } else {
                    break;
                }
            }

        }

        private void OnPathComplete (Path p) {
            if (!p.error) {
                _path = p;
                _indexWaypoint = 0;
                ClampPath ();
            }
        }
        
    }
}