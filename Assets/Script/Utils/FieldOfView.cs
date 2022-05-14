using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class FieldOfView : MonoBehaviour
{
    public List<Transform> VisibleTargets { get { return _visibleTargets; } }

    public float viewRadius;
    [Range (0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstableMask;

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    private List<Transform> _visibleTargets = new List<Transform>();

    private void Start () 
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        // viewMeshFilter.mesh = viewMesh;
        StartCoroutine ("FindTargetWithDelay", .2f);
    }

    IEnumerator FindTargetWithDelay (float delay) 
    {
        while (true) 
        {
            yield return new WaitForSeconds (delay);
            FindVisibleTargets ();
        }
    }

    private void Update () {
        DrawFieldOfView ();
    }

    void FindVisibleTargets () 
    {
        _visibleTargets.Clear ();
        Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll (transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++) 
        {
            Transform target = targetInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle (transform.up, dirToTarget);

            if (angleToTarget < viewAngle && angleToTarget > -viewAngle) 
            {
                float distanceToTarget = Vector3.Distance (transform.position, target.position);

                if (!Physics2D.Raycast (transform.position, dirToTarget, distanceToTarget, obstableMask))
                {
                    _visibleTargets.Add (target);
                }
            }
        }
    }

    private void DrawFieldOfView () {
        int stepCount = Mathf.RoundToInt (viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo newViewCast;

        for (int i = 0; i <= stepCount; i++) {
            float angle = -transform.eulerAngles.z - viewAngle + stepAngleSize * i;
            newViewCast = ViewCast (angle);
            viewPoints.Add (newViewCast.Point);

            newViewCast = ViewCast (angle + viewAngle);
            viewPoints.Add (newViewCast.Point);
        }

        for (int i = 0; i < viewPoints.Count; i++) {
            Debug.DrawLine (transform.position, viewPoints[i], Color.red);
        }

        // int vertexCount = viewPoints.Count + 1;
        // Vector3[] vertices = new Vector3 [vertexCount];
        // int[] triangles = new int[(vertexCount - 2) * 3];

        // vertices[0] = Vector3.zero;
        // for (int i = 0; i < vertexCount - 1; i++) {
        //     vertices[i + 1] = viewPoints [i];

        //     if (i < vertexCount - 2) {
        //         triangles[i * 3] = 0;
        //         triangles[i * 3 + 1] = i + 1;
        //         triangles[i * 3 + 2] = i + 2;
        //     }
        // }

        // viewMesh.Clear ();
        // viewMesh.vertices = vertices;
        // viewMesh.triangles = triangles;
        // viewMesh.RecalculateNormals ();
    }
    
    private ViewCastInfo ViewCast (float globalAngle) {
        Vector3 dir = Tools.DirFromAngle2D (globalAngle);
        RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, viewRadius, obstableMask);

        if (hit) {
            return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
        } else {
            return new ViewCastInfo (false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo {
        public bool Hit;
        public Vector3 Point;
        public float Distance;
        public float Angle;

        public ViewCastInfo (bool hit, Vector3 point, float distance, float angle) {
            Hit = hit;
            Point = point;
            Distance = distance;
            Angle = angle;
        }
    }
}