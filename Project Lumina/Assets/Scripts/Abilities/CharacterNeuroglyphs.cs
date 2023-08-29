using System.Collections.Generic;
using System.Linq;
using ProjectLumina.Data;
using ProjectLumina.Neuroglyphs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Neuroglyphs")]
    public class CharacterNeuroglyphs : MonoBehaviour
    {
        [Range(0, 25), SerializeField]
        private int _maximumSlots;

        [RequiredListLength("@this._maximumSlots"), SerializeField]
        private List<NeuroglyphSlot> _neuroglyphSlots;

        public UnityAction onNeuroglyphListRefresh;

        private void Start()
        {
            UpdateNeuroglyphs();
        }

        public void AddNeuroglyph(Neuroglyph neuroglyph)
        {
            GetAvailableSlot(neuroglyph).SetSlot(neuroglyph);
            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void RemoveNeuroglyph(Neuroglyph neuroglyph)
        {
            if (HasNeuroglyph(neuroglyph))
            {
                GetSlot(neuroglyph).SetSlot(null);
            }

            UpdateNeuroglyphs();

            onNeuroglyphListRefresh?.Invoke();
        }

        public void UpdateNeuroglyphs()
        {
            foreach (NeuroglyphSlot neuroglyphSlot in _neuroglyphSlots)
            {
                neuroglyphSlot.RevertSlot(gameObject);
                neuroglyphSlot.ApplySlot(gameObject);
            }
        }

        public bool HasNeuroglyph(Neuroglyph neuroglyph)
        {
            foreach (var slot in _neuroglyphSlots.Where(slot => slot.Neuroglyph != null && slot.Neuroglyph.NeuroglyphID == neuroglyph.NeuroglyphID))
            {
                return slot.Neuroglyph;
            }

            return false;
        }

        public NeuroglyphSlot GetAvailableSlot(Neuroglyph neuroglyph)
        {
            if (!HasNeuroglyph(neuroglyph))
            {
                return GetFirstEmptySlot();
            }
            else
            {
                return GetSlot(neuroglyph);
            }
        }

        public NeuroglyphSlot GetFirstEmptySlot()
        {
            return _neuroglyphSlots.First(i => i.Neuroglyph == null);
        }

        public NeuroglyphSlot GetSlot(Neuroglyph neuroglyph)
        {
            return _neuroglyphSlots.First(i => i.Neuroglyph.NeuroglyphID == neuroglyph.NeuroglyphID);
        }

        public NeuroglyphSlot[] GetSlots()
        {
            return _neuroglyphSlots.ToArray();
        }
    }
}