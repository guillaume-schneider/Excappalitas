namespace Utils {
    public static class Math {
        public static float InverseInterpolation(float min, float max, float value) {
            return ((min + value) / (max - min));
        }

        public static float Interpolation(float min, float max, float t) {
            return ((1.0f - t) * min + max * t);
        }
    }
}