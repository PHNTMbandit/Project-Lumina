using ProjectLumina.Neuroglyphs;
using UnityEngine;

namespace ProjectLumina.Factories
{
    public class NeuroglyphFactory
    {
        public Neuroglyph GetNeuroglyph(Neuroglyph neuroglyph)
        {
            return Object.Instantiate(neuroglyph);
        }
    }
}