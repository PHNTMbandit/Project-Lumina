using System.Collections.Generic;
using ProjectLumina.Abilities;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class NeuroglyphList : MonoBehaviour
    {
        [SerializeField]
        private CharacterNeuroglyphs _characterNeuroglyphs;

        [SerializeField]
        private NeuroglyphIcon _templateNeuroglyphIcon;

        [SerializeField]
        private Transform _transform;

        private readonly List<NeuroglyphIcon> _icons = new();

        private void Awake()
        {
            _templateNeuroglyphIcon.gameObject.SetActive(false);
        }

        public void GenerateList(Neuroglyph[] neuroglyphs)
        {
            ResetList();

            foreach (Neuroglyph statusEffect in neuroglyphs)
            {
                NeuroglyphIcon neuroglyphIcon = Instantiate(_templateNeuroglyphIcon.gameObject, _transform).GetComponent<NeuroglyphIcon>();
                neuroglyphIcon.gameObject.SetActive(true);
                neuroglyphIcon.SetIcon(statusEffect.Icon);

                _icons.Add(neuroglyphIcon);
            }
        }

        private void ResetList()
        {
            if (_icons.Count > 0)
            {
                foreach (NeuroglyphIcon neuroglyphIcon in _icons)
                {
                    Destroy(neuroglyphIcon.gameObject);
                }

                _icons.Clear();
            }
        }
    }
}