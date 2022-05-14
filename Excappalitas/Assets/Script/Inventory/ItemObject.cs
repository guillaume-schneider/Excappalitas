using Excappalitas.Interactable;
using Excappalitas.Inventory;
using Excappalitas.Events;
using UnityEngine;

namespace Excappalitas.Item {
    public abstract class ItemObject : MonoBehaviour, IInteractable
    {
        public InventoryItemData metaData;

        private void OnEnable() => InteractableEventManager.OnUpdate += OnInteract;
        private void OnDisable() => InteractableEventManager.OnUpdate -= OnInteract;

        public void OnInteract(InteractionMessage message) {
            if (HasMessageInput(message) && IsThisObjectID(message.InteractableID)) {
                OnPickItem(message.Caller);
                AddItemToInventory(message.Caller.GetComponent<InventorySystem>());
            }
        }

        public abstract void OnPickItem(Transform picker);

        private void AddItemToInventory(InventorySystem inventory) {
            inventory.Add(metaData);
            Destroy(gameObject);
        }

        private bool HasMessageInput(InteractionMessage message)
            => message.GetInputByName("HoldInput").HasInput;

        private bool IsThisObjectID(int objectID) => objectID == this.gameObject.GetInstanceID();

    }
}
