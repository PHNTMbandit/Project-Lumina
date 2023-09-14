using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [TextArea, SerializeField]
        private string _eventDescription;

        [Space, SerializeField]
        private GameEvent _event;

        [Space, SerializeField]
        private UnityEvent _responses;

        private void OnEnable()
        {
            if (_event != null)
            {
                RegisterListener();
            }
        }

        private void OnDisable()
        {
            if (_event != null)
            {
                UnregisterListener();
            }
        }

        public void RegisterListener()
        {
            _event.RegisterListener(this);
        }

        public void UnregisterListener()
        {
            _event.UnregisterListener(this);
        }

        public void SetEvent(GameEvent gameEvent)
        {
            _event = gameEvent;
        }

        public void OnEventRaised()
        {
            _responses?.Invoke();
        }
    }
}