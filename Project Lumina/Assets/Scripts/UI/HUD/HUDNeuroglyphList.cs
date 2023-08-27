using System.Collections.Generic;
using ProjectLumina.Abilities;
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
        private HUDNeuroglyphIcon _templateBlessingNeuroglyphIcon, _templateHexNeuroglyphIcon;

        [SerializeField]
        private Transform _buffTransform, _debuffTransform;

        private readonly List<HUDNeuroglyphIcon> _icons = new();

        private void Awake()
        {
            _templateBlessingNeuroglyphIcon.gameObject.SetActive(false);
            _templateHexNeuroglyphIcon.gameObject.SetActive(false);

            _characterNeuroglyphs.onNeuroglyphListRefresh += GenerateList;
        }

        private void Start()
        {
            GenerateList();
        }

        public void GenerateList()
        {
            ResetList();

            foreach (Neuroglyph neuroglyph in _characterNeuroglyphs.GetNeuroglyphsByType(NeuroglyphType.Blessing))
            {
                HUDNeuroglyphIcon icon = Instantiate(_templateBlessingNeuroglyphIcon.gameObject, _buffTransform).GetComponent<HUDNeuroglyphIcon>();
                icon.gameObject.SetActive(true);
                icon.SetIcon(neuroglyph.Icon);
                icon.SetTierImage(_controller.GetTierSprite(neuroglyph.CurrentTierLevel));

                _icons.Add(icon);
            }

            foreach (Neuroglyph neuroglyph in _characterNeuroglyphs.GetNeuroglyphsByType(NeuroglyphType.Hex))
            {
                HUDNeuroglyphIcon icon = Instantiate(_templateHexNeuroglyphIcon.gameObject, _debuffTransform).GetComponent<HUDNeuroglyphIcon>();
                icon.gameObject.SetActive(true);
                icon.SetIcon(neuroglyph.Icon);
                icon.SetTierImage(_controller.GetTierSprite(neuroglyph.CurrentTierLevel));

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