using UnityEngine;

namespace Utils {
    public static class AITools {
        public static Vector3 Seek (Vector3 origin, Vector3 target, Vector3 currentSpeed, float maxSpeed) {
            Vector3 desiredVelocity = (target - origin).normalized * maxSpeed;
            return (desiredVelocity - new Vector3 (currentSpeed.x, currentSpeed.y, 0.0f));
        }
    }
}