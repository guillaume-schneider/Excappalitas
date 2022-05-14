using Excappalitas.Inputs;

namespace Excappalitas.Interactable {
    public interface IInteractable {
        public abstract void OnInteract(InteractionMessage message);
    }
}