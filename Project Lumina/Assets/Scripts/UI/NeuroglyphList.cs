using System.Collections.Generic;
using ProjectLumina.Character;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class NeuroglyphList : MonoBehaviour
    {
        [SerializeField]
        private NeuroglyphController _controller;

        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private NeuroglyphButton _templateNeuroglyphIcon;

        [SerializeField]
        private Transform _transform;

        private readonly List<NeuroglyphButton> _buttons = new();

        private void Awake()
        {
            _templateNeuroglyphIcon.gameObject.SetActive(false);
        }

        public void GenerateList(Neuroglyph[] neuroglyphs)
        {
            ResetList();

            foreach (Neuroglyph neuroglyph in neuroglyphs)
            {
                NeuroglyphButton button = Instantiate(_templateNeuroglyphIcon.gameObject, _transform).GetComponent<NeuroglyphButton>();
                button.gameObject.SetActive(true);

                button.SetIcon(neuroglyph.Icon);
                button.SetDescription(neuroglyph.Description);
                button.SetName(neuroglyph.NeuroglyphName);
                button.SetNeuroglyph(neuroglyph);
                button.SetTierImage(_controller.GetTierSprite(neuroglyph.CurrentTierLevel));

                _buttons.Add(button);
            }
        }

        private void ResetList()
        {
            if (_buttons.Count > 0)
            {
                foreach (NeuroglyphButton neuroglyphIcon in _buttons)
                {
                    Destroy(neuroglyphIcon.gameObject);
                }

                _buttons.Clear();
            }
        }
    }
}