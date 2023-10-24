using System.Collections.Generic;
using System.Linq;
using ProjectLumina.Neuroglyphs;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Neuroglyphs")]
    public class CharacterNeuroglyphs : MonoBehaviour
    {
        [field: Range(1, 10), SerializeField]
        public int NeuroglyphAmount { get; private set; }

        [field: SerializeField]
        public List<Neuroglyph> Neuroglyphs { get; private set; }

        public UnityAction onNeuroglyphListRefresh;

        private void Start()
        {
            UpdateNeuroglyphs();
        }

        public void AddNeuroglyph(Neuroglyph neuroglyph)
        {
            if ((Neuroglyphs.Count + 1) <= NeuroglyphAmount)
            {
                Neuroglyphs.Add(neuroglyph);
            }

            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void RemoveNeuroglyph(Neuroglyph neuroglyph)
        {
            if (HasNeuroglyph(neuroglyph))
            {
                Neuroglyphs.Remove(neuroglyph);
            }

            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void UpdateNeuroglyphs()
        {
            foreach (Neuroglyph neuroglyph in Neuroglyphs)
            {
                neuroglyph.Revert(gameObject);
                neuroglyph.Apply(gameObject);
            }
        }

        public Neuroglyph GetNeuroglyph(Neuroglyph neuroglyph)
        {
            return Neuroglyphs.First(i => i == neuroglyph);
        }

        public Neuroglyph GetNeuroglyph(int ID)
        {
            return Neuroglyphs.First(i => i.NeuroglyphID == ID);
        }

        public Neuroglyph GetNeuroglyph(string name)
        {
            return Neuroglyphs.First(i => i.NeuroglyphName == name);
        }

        public bool HasNeuroglyph(Neuroglyph neuroglyph)
        {
            return Neuroglyphs.Any(i => i != null && i.NeuroglyphID == neuroglyph.NeuroglyphID);
        }
    }
}