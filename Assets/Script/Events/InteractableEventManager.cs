using System;
using Excappalitas.Interactable;
using UnityEngine;

namespace Excappalitas.Events {
    public static class InteractableEventManager {

        public static event Action<InteractionMessage> OnUpdate;

        public static void Update(InteractionMessage message) {
            if (OnUpdate != null) OnUpdate(message);
        }
    }
}