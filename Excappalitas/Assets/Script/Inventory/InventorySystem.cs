using UnityEngine;
using System.Collections.Generic;

namespace Excappalitas.Inventory {
    public class InventorySystem : MonoBehaviour {
        private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
        public List<InventoryItem> Inventory { get; private set; }

        private void Awake() {
            Inventory = new List<InventoryItem>();
            _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        }

        public InventoryItem Get(InventoryItemData referenceData) {
            if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) return value;
            return null;
        }

        public void Add(InventoryItemData referenceData) {
            if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) value.AddToStack();
            else {
                InventoryItem newItem = new InventoryItem(referenceData);
                Inventory.Add(newItem);
                _itemDictionary.Add(referenceData, newItem);
            }
        }

        public void Remove(InventoryItemData referenceData) {
            if (_itemDictionary.TryGetValue(referenceData, out InventoryItem value)) {
                value.RemoveFromStack();

                if (value.StackSize == 0) {
                    Inventory.Remove(value);
                    _itemDictionary.Remove(referenceData);
                }
            }
        }
    }
}
