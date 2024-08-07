using DG.Tweening;
using ProjectLumina.Capabilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class InteractUI : MonoBehaviour
    {
        [BoxGroup("Tween"), Range(0, 5), SerializeField]
        private float _showFadeDuration,
            _hideFadeDuration;

        [BoxGroup("References"), SerializeField]
        private GameObject _parent;

        [BoxGroup("References"), SerializeField]
        private TextMeshProUGUI _text;

        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            _canvas.alpha = 0;
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

        public void Show(Interactable interactable)
        {
            _text.SetText(interactable.InteractText);
            _canvas.DOFade(1, _showFadeDuration);
        }

        public void Hide()
        {
            _canvas.DOFade(0, _hideFadeDuration);
        }
    }
}
