using System.Collections.Generic;
using ProjectLumina.Abilities;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphList : MonoBehaviour
    {
        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private HUDNeuroglyphIcon _templateNeuroglyphIcon;

        [SerializeField]
        private Transform _buffTransform, _debuffTransform;

        private readonly List<HUDNeuroglyphIcon> _icons = new();

        private void Awake()
        {
            _templateNeuroglyphIcon.gameObject.SetActive(false);
        }

        public void GenerateList()
        {
            ResetList();

            foreach (Neuroglyph statusEffect in _characterNeuroglyphs.GetNeuroglyphsByType(NeuroglyphType.Blessing))
            {
                HUDNeuroglyphIcon neuroglyphIcon = Instantiate(_templateNeuroglyphIcon.gameObject, _buffTransform).GetComponent<HUDNeuroglyphIcon>();
                neuroglyphIcon.gameObject.SetActive(true);
                neuroglyphIcon.SetIcon(statusEffect.Icon);

                _icons.Add(neuroglyphIcon);
            }

            foreach (Neuroglyph statusEffect in _characterNeuroglyphs.GetNeuroglyphsByType(NeuroglyphType.Hex))
            {
                HUDNeuroglyphIcon neuroglyphIcon = Instantiate(_templateNeuroglyphIcon.gameObject, _debuffTransform).GetComponent<HUDNeuroglyphIcon>();
                neuroglyphIcon.gameObject.SetActive(true);
                neuroglyphIcon.SetIcon(statusEffect.Icon);

                _icons.Add(neuroglyphIcon);
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