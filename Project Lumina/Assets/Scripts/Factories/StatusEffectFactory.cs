using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.Factories
{
    public class StatusEffectFactory
    {
        public Neuroglyph GetStatusEffect(Neuroglyph neuroglyph)
        {
            return ScriptableObject.CreateInstance(neuroglyph.name) as Neuroglyph;
        }
    }
}