using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Excappalitas.AI {
    public class TargetFinder : MonoBehaviour {
        public Transform ChosenTarget { get { return nearestTarget; } }

        private LayerMask obstacle;

        private FieldOfView fov;
        private List<Transform> targets;
        private Transform nearestTarget;
        private Transform farestTarget;

        private void Start () {
            fov = GetComponent<FieldOfView>();
            obstacle = LayerMask.GetMask ("Obstacle");
            farestTarget = GameObject.Find ("FarestGameObject").transform;
            nearestTarget = farestTarget;
        }

        private void Update () {
            UpdateTargets ();
            GetNearestTarget ();
            DrawNearestTarget ();
        }

        public void SetTarget(Transform target) => nearestTarget = target;

        private void UpdateTargets () {
            targets = fov.VisibleTargets;
        }   

        private void GetNearestTarget () {
            if (targets.Count > 0) {
                for (int i = 0 ; i < targets.Count; i++) {
                    float distanceToTarget = (targets[i].position - transform.position).sqrMagnitude;
                    float distanceToNearest = (nearestTarget.position - transform.position).sqrMagnitude;

                    if (distanceToTarget < distanceToNearest) {
                        nearestTarget = targets[i];
                    } 
                }
            }
        }

        private void DrawNearestTarget () {
            Debug.DrawLine (transform.position, nearestTarget.position, Color.green);
        }

    }
}