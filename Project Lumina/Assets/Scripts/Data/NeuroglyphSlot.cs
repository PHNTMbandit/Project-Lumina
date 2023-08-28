using System;
using ProjectLumina.Neuroglyphs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable]
    public class NeuroglyphSlot
    {
        [ReadOnly, ShowInInspector]
        public Neuroglyph Neuroglyph { get; private set; }

        public void SetSlot(Neuroglyph neuroglyph)
        {
            Neuroglyph = neuroglyph;
        }

        public void ApplySlot(GameObject target)
        {
            if (Neuroglyph != null)
            {
                Neuroglyph.Apply(target);
            }
        }

        public void RevertSlot(GameObject target)
        {
            if (Neuroglyph != null)
            {
                Neuroglyph.Revert(target);
            }
        }
    }
}