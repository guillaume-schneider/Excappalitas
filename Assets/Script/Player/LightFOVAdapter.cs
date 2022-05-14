using UnityEngine.Experimental.Rendering.Universal;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Excappalitas.Player {
    public class LightFOVAdapter : MonoBehaviour {

        private Light2D lightObject;
        private FieldOfView playerFOV;

        private void Start() {
            lightObject = GetComponent<Light2D>();
            playerFOV = GetComponent<FieldOfView>();
            ApplyFOVParametersToLight();
        }

        private void Update() {
            ApplyFOVParametersToLight();
        }

        public void ApplyFOVParametersToLight() {
            lightObject.pointLightOuterAngle = playerFOV.viewAngle * 2;
            lightObject.pointLightOuterRadius = playerFOV.viewRadius;
            lightObject.lightType = Light2D.LightType.Point;
            ApplySortingLayer(Utils.Tools.GetAllSortingLayerIDExcept("Conditionnal", "Obstacle"));
        }

        private void ApplySortingLayer(int[] layers) {
            FieldInfo fieldInfo = lightObject.GetType().GetField("m_ApplyToSortingLayers", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(lightObject, layers);
        }
    }
}