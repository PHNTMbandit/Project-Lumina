using System.Collections.Generic;
using System.Linq;
using ProjectLumina.Neuroglyphs;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Neuroglyphs")]
    public class CharacterNeuroglyphs : MonoBehaviour
    {
        [SerializeField]
        private List<Neuroglyph> _activeNeuroglyphs;

        public UnityAction onNeuroglyphListRefresh;

        private void Start()
        {
            UpdateNeuroglyphs();
        }

        public void AddNeuroglyph(Neuroglyph neuroglyph)
        {
            if (!HasNeuroglyph(neuroglyph))
            {
                _activeNeuroglyphs.Add(neuroglyph);
            }
            else
            {
                RemoveNeuroglyph(GetNeuroglyph(neuroglyph.NeuroglyphID));
                _activeNeuroglyphs.Add(neuroglyph);
            }

            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void RemoveNeuroglyph(Neuroglyph neuroglyph)
        {
            if (HasNeuroglyph(neuroglyph))
            {
                _activeNeuroglyphs.Remove(neuroglyph);
            }

            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void UpdateNeuroglyphs()
        {
            foreach (Neuroglyph neuroglyph in _activeNeuroglyphs)
            {
                neuroglyph.Revert(gameObject);
                neuroglyph.Apply(gameObject);
            }
        }

        public Neuroglyph GetNeuroglyph(string neuroglyph)
        {
            return _activeNeuroglyphs.Find(i => i.NeuroglyphName == neuroglyph);
        }

        public Neuroglyph GetNeuroglyph(int ID)
        {
            return _activeNeuroglyphs.Find(i => i.NeuroglyphID == ID);
        }

        public Neuroglyph GetNeuroglyph(Neuroglyph neuroglyph)
        {
            return _activeNeuroglyphs.Find(i => i.NeuroglyphID == neuroglyph.NeuroglyphID);
        }

        public Neuroglyph GetNeuroglyphs(Neuroglyph neuroglyph)
        {
            return _activeNeuroglyphs.Find(i => i == neuroglyph);
        }

        public Neuroglyph[] GetNeuroglyphsByType(NeuroglyphType neuroglyphType)
        {
            return _activeNeuroglyphs.FindAll(i => i.NeuroglyphType == neuroglyphType).OrderByDescending(n => n.NeuroglyphName).ToArray();
        }

        public bool HasNeuroglyph(Neuroglyph neuroglyph)
        {
            return _activeNeuroglyphs.Exists(i => i.NeuroglyphID == neuroglyph.NeuroglyphID);
        }
    }
}