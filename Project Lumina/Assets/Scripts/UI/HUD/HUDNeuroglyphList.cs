using System.Collections.Generic;
using ProjectLumina.Abilities;
using ProjectLumina.Data;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphList : MonoBehaviour
    {
        [SerializeField]
        private NeuroglyphController _controller;

        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private HUDNeuroglyphIcon _templateNeuroglyphIcon;

        [SerializeField]
        private Transform _transform;

        private readonly List<HUDNeuroglyphIcon> _icons = new();

        private void Awake()
        {
            _templateNeuroglyphIcon.gameObject.SetActive(false);

            _characterNeuroglyphs.onNeuroglyphListRefresh += GenerateList;
        }

        private void Start()
        {
            GenerateList();
        }

        public void GenerateList()
        {
            ResetList();

            foreach (NeuroglyphSlot slot in _characterNeuroglyphs.GetSlots())
            {
                HUDNeuroglyphIcon icon = Instantiate(_templateNeuroglyphIcon.gameObject, _transform).GetComponent<HUDNeuroglyphIcon>();
                icon.gameObject.SetActive(true);

                if (slot.Neuroglyph != null)
                {
                    icon.SetIcon(slot.Neuroglyph.Icon);
                    icon.SetTierImage(_controller.GetTierSprite(slot.Neuroglyph.CurrentTierLevel));
                }
                else
                {
                    icon.SetTierImage(null);
                }

                _icons.Add(icon);
            }
        }

        private void ResetList()
        {
            if (_icons.Count > 0)
            {
                foreach (HUDNeuroglyphIcon neuroglyphIcon in _icons)
                {
                    Destroy(neuroglyphIcon.gameObject);
                }

                _icons.Clear();
            }
        }
    }
}