using Excappalitas.Interactable;
using UnityEngine.Experimental.Rendering.Universal;
using Excappalitas.Player;
using UnityEngine;

namespace Excappalitas.Item {
    public class FlashlightItem : ItemObject {

        public override void OnPickItem(Transform picker) {
            AddFlashlightToGameObject(picker.gameObject);
        }

        private void AddFlashlightToGameObject(GameObject gameObject) {
            gameObject.AddComponent<Light2D>();
            gameObject.AddComponent<LightFOVAdapter>();
        }

    }
}