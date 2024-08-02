using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using ProjectLumina.Player.Input;
using ProjectLumina.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Interactor")]
    public class CharacterInteractor : MonoBehaviour
    {
        public UnityEvent<Interactable> onInteractableDetected;
        public UnityEvent onInteractableLost,
            onInteract;

        [FoldoutGroup("References"), SerializeField]
        private InputReader _inputReader;

        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        [FoldoutGroup("References"), SerializeField]
        private InteractUI _interactUI;

        private void OnEnable()
        {
            _sensor.OnDetected.AddListener(OnInteractableDetected);
            _sensor.OnLostDetection.AddListener(OnInteractableLost);
            _inputReader.onInteract += OnInteract;
        }

        private void OnDisable()
        {
            _sensor.OnDetected.RemoveListener(OnInteractableDetected);
            _sensor.OnLostDetection.RemoveListener(OnInteractableLost);
            _inputReader.onInteract -= OnInteract;
        }

        private void Update()
        {
            var interactable = _sensor.GetNearestComponent<Interactable>();

            if (interactable != null && interactable.IsInteractable)
            {
                _interactUI.gameObject.SetActive(true);
                _interactUI.SetInteractText(interactable);
            }
            else
            {
                _interactUI.gameObject.SetActive(false);
            }
        }

        public void OnInteract()
        {
            var interactable = _sensor.GetNearestComponent<Interactable>();

            if (interactable != null && interactable.IsInteractable)
            {
                interactable.Interact(gameObject);
            }
        }

        public void OnInteractableDetected(GameObject gameObject, Sensor sensor)
        {
            if (gameObject != null)
            {
                if (gameObject.TryGetComponent(out Interactable interactable))
                {
                    interactable.OnDetected(gameObject);

                    onInteractableDetected?.Invoke(interactable);
                }
            }
        }

        public void OnInteractableLost(GameObject gameObject, Sensor sensor)
        {
            if (gameObject != null)
            {
                if (gameObject.TryGetComponent(out Interactable interactable))
                {
                    interactable.OnLost(gameObject);

                    onInteractableLost?.Invoke();
                }
            }
        }
    }
}
