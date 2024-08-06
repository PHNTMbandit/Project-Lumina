using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [AddComponentMenu("Capabilities/Interactable")]
    public class Interactable : MonoBehaviour
    {
        public bool IsInteractable { get; private set; } = true;

        [field: SerializeField]
        public string InteractText { get; private set; }

        public UnityEvent<GameObject> onDetected,
            onInteracted,
            onLost;

        public void OnDetected(GameObject interactor)
        {
            onDetected?.Invoke(interactor);
        }

        public void Interact(GameObject interactor)
        {
            onInteracted?.Invoke(interactor);
        }

        public void OnLost(GameObject interactor)
        {
            onLost?.Invoke(interactor);
        }

        public void SetInteractable(bool interactable)
        {
            IsInteractable = interactable;
        }
    }
}
