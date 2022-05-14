using UnityEditor;
using UnityEngine;

namespace Utils {

    [CustomEditor (typeof (FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI() {
            FieldOfView fov = (FieldOfView)target;
            Handles.color = Color.white;
            Handles.DrawWireArc (fov.transform.position, Vector3.forward, Vector3.up, 360, fov.viewRadius);

            Vector3 viewAngleA = Tools.DirFromAngleGlobal2D(-fov.viewAngle, fov.transform);
            Vector3 viewAngleB = Tools.DirFromAngleGlobal2D(fov.viewAngle, fov.transform);

            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

            Handles.color = Color.red;
            foreach (Transform visibleTarget in fov.VisibleTargets) 
            {
                Handles.DrawLine (fov.transform.position, visibleTarget.position);
            }
        }

    }
}
