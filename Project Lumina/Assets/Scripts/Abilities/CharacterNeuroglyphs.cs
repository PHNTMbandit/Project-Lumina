using System.Collections.Generic;
using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Neuroglyphs")]
    public class CharacterNeuroglyphs : MonoBehaviour
    {
        [SerializeField]
        private List<Neuroglyph> _activeNeuroglyphs;

        private void Start()
        {
            UpdateNeuroglyphs();
        }

        public void AddNeuroglyph(Neuroglyph neuroglyph)
        {
            if (!_activeNeuroglyphs.Contains(neuroglyph))
            {
                _activeNeuroglyphs.Add(neuroglyph);

                neuroglyph.Apply(gameObject);
            }
        }

        public void RemoveNeuroglyph(Neuroglyph neuroglyph)
        {
            if (_activeNeuroglyphs.Contains(neuroglyph))
            {
                _activeNeuroglyphs.Remove(neuroglyph);

                neuroglyph.Revert(gameObject);
            }
        }

        public void UpdateNeuroglyphs()
        {
            foreach (Neuroglyph statusEffect in _activeNeuroglyphs)
            {
                statusEffect.Revert(gameObject);
                statusEffect.Apply(gameObject);
            }
        }

        public Neuroglyph GetNeuroglyph(string neuroglyph)
        {
            return _activeNeuroglyphs.Find(i => i.NeuroglyphName == neuroglyph);
        }

        public Neuroglyph GetNeuroglyphs(Neuroglyph neuroglyph)
        {
            return _activeNeuroglyphs.Find(i => i == neuroglyph);
        }

        public Neuroglyph[] GetNeuroglyphsByType(NeuroglyphType neuroglyphType)
        {
            return _activeNeuroglyphs.FindAll(i => i.NeuroglyphType == neuroglyphType).ToArray();
        }
    }
}