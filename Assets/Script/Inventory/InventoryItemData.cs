using UnityEngine;

namespace Excappalitas.Inventory {
    [CreateAssetMenu(menuName = "Inventory Item Data")]
    public class InventoryItemData : ScriptableObject {
        public string Id;
        public string DisplayName;
        public Sprite Icon;
        public GameObject Prefab;
    }
}
