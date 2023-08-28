using ProjectLumina.Neuroglyphs;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphSlot : MonoBehaviour
    {
        public Neuroglyph Neuroglyph { get; private set; }

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Image _tierImage;

        private float _originalIconAlpha, _originalTierAlpha;

        private void Awake()
        {
            _originalIconAlpha = _icon.color.a;
            _originalTierAlpha = _tierImage.color.a;
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetNeuroglyph(Neuroglyph neuroglyph)
        {
            Neuroglyph = neuroglyph;
        }

        public void SetTierImage(Sprite sprite)
        {
            if (sprite != null)
            {
                _tierImage.enabled = true;
                _tierImage.sprite = sprite;
            }
            else
            {
                _tierImage.enabled = false;
            }
        }

        public void Highlight()
        {
            _icon.color = new Color(1, 1, 1, 1);
            _tierImage.color = new Color(1, 1, 1, 1);
        }

        public void Unhighlight()
        {
            _icon.color = new Color(1, 1, 1, _originalIconAlpha);
            _tierImage.color = new Color(1, 1, 1, _originalTierAlpha);
        }
    }
}