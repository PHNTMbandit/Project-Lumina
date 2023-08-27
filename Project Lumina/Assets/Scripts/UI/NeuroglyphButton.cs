using ProjectLumina.Abilities;
using ProjectLumina.Neuroglyphs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class NeuroglyphButton : MonoBehaviour
    {
        public Neuroglyph Neuroglyph { get; private set; }

        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Image _tierImage;

        [SerializeField]
        private TextMeshProUGUI _nameText, _descriptionText;

        public void OnClick()
        {
            _characterNeuroglyphs.AddNeuroglyph(Neuroglyph);
        }

        public void SetDescription(string description)
        {
            _descriptionText.SetText(description);
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetName(string name)
        {
            _nameText.SetText(name);
        }

        public void SetNeuroglyph(Neuroglyph neuroglyph)
        {
            Neuroglyph = neuroglyph;
        }

        public void SetTierImage(Sprite sprite)
        {
            _tierImage.sprite = sprite;
        }
    }
}