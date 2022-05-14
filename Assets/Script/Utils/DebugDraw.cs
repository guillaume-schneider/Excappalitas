using UnityEngine;

namespace Utils {
    public static class DebugDraw {
        public static void DrawCircle (float radius, float resolution) {
            Vector2[] points = new Vector2[(int)resolution];
            double step = 360d / resolution;
            int j = 0;
            for (double i = 0; i <= 360; i += step) {
                points[j] = new Vector2 (Mathf.Cos ((float)(radius * step)), Mathf.Sin ((float)(radius * step)));
                j++;
            }

            for (int i = 0; i < points.Length - 1; i++) {
                Debug.DrawLine (points[i], points[i + 1], Color.yellow);
            }
        }
    }
}