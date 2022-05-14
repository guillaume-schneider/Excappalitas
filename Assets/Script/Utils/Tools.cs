using UnityEngine;

namespace Utils 
{
    public static class Tools 
    {
        public static void FlipSprite(Rigidbody2D rigidbody, Transform transform) 
            => transform.localScale = new Vector2 (-Mathf.Sign(rigidbody.velocity.x), 1f);

        public static Vector3 DirFromAngle2D(float angleInDegrees) {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 
                               Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
        }

        public static Vector3 DirFromAngleGlobal2D(float angleInDegrees, Transform transform) {
            angleInDegrees -= transform.eulerAngles.z;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 
                               Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
        }

        public static Vector3 DirFromAngle(float angleInDegrees) {
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, 
                               Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public static Vector3 DirFromAngleGlobal(float angleInDegrees, Transform transform) {
            angleInDegrees += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, 
                                         Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public static bool EqualsPos (Vector3 p1, Vector3 p2) {
            Vector3Round (ref p1);
            Vector3Round (ref p2);
            return (p1.x == p2.x && p1.y == p2.y && p1.z == p2.z);
        }

        public static void Vector3Round (ref Vector3 v) {
            v.x = Mathf.Round (v.x);
            v.y = Mathf.Round (v.y);
            v.z = Mathf.Round (v.z);
        }

        /// <sumary>
        /// Return the Cosinus between two vectors
        /// Using the dot product relation :
        /// v1 . v2 = cos(α) * ||v1|| * ||v2||
        /// </sumary>
        public static float Vector3Cos (Vector3 v1, Vector3 v2) {
            return Vector3.Dot (v1, v2) / (v1.magnitude * v2.magnitude);
        }

        /// <sumary>
        /// Return the Sinus between two vectors
        /// Using the cross product relation :
        /// || v1 /\ v2 || = sin(α) * ||v1|| * ||v2||
        /// </sumary>
        public static float Vector3Sin (Vector3 v1, Vector3 v2) {
            return Vector3.Cross (v1, v2).magnitude / (v1.magnitude * v2.magnitude);
        }

        /// <sumary>
        /// Method used in 2D space
        /// Convert Vector2 into Vector3
        /// Keep x and y and z = 0.0
        /// </sumary>
        public static Vector3 V2ToV3 (Vector2 v) {
            return (new Vector3 (v.x, v.y, 0.0f));
        }

        /// <sumary>
        /// Method used in 2D space
        /// Convert Vector3 into Vector2
        /// Keep x and y but loss z
        /// </sumary>
        public static Vector2 V3ToV2 (Vector3 v) {
            return (new Vector2 (v.x, v.y));
        }

        public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
        {
            transform.localScale = matrix.lossyScale;
            transform.rotation = matrix.rotation;
            transform.position = (Vector3)matrix.transpose.GetColumn (4);
        }

        public static void CopyArray<T>(T[] origin, out T[] destination) {
            destination = new T[origin.Length];
            for (int i = 0; i < origin.Length; i++) {
                destination[i] = origin[i];
            }
        }

        public static T[] ArrayExpansion<T>(T[] array) {
            T[] res = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++) {
                res[i] = array[i];
            }
            return res;
        }

        public static void Void() {}

        public static double Max(double[] values) {
            double max = double.MinValue;
            foreach (var value in values) {
                if (value > max) max = value;
            }

            return max;
        }

        public static int[] GetAllSortingLayerIDExcept(params string[] layersName) {
            System.Collections.Generic.List<int> layersID = new System.Collections.Generic.List<int>(GetAllSortingLayerID());

            foreach (var layerName in layersName) {
                layersID.Remove(SortingLayer.NameToID(layerName));
            }

            return layersID.ToArray();
        }

        public static int[] GetAllSortingLayerID() {
            var layers = SortingLayer.layers;
            int[] layersID = new int[layers.Length];

            for (int i = 0; i < layersID.Length; i++) {
                layersID[i] = layers[i].id;
            }

            return layersID;
        }
    }
}