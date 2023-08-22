using ProjectLumina.Capabilities;
using TMPro;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class InteractUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _parent;

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                if (_parent.transform.localScale.x < 0)
                {
                    transform.rotation = new Quaternion(0, -180, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.identity;
                }
            }
        }

        public void SetInteractText(Interactable interactable)
        {
            _text.SetText(interactable.InteractText);
        }
    }
}