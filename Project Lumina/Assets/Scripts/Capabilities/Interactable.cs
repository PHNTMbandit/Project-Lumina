using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField]
        public string InteractText { get; private set; }

        public UnityEvent onInteracted;

        public void Interact()
        {
            onInteracted?.Invoke();
        }
    }
}