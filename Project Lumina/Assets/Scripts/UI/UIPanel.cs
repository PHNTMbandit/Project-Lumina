using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField, ToggleLeft]
        private bool _openOnStart;

        [Space]
        public UnityEvent OnOpen, OnClose;

        private void Awake()
        {
            if (_openOnStart)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public void Toggle()
        {
            if (gameObject.activeSelf)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public void Open()
        {
            gameObject.SetActive(true);

            OnOpen?.Invoke();
        }

        public void Close()
        {
            gameObject.SetActive(false);

            OnClose?.Invoke();
        }
    }
}