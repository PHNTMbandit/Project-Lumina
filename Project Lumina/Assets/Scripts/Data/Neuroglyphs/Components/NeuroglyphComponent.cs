using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    public abstract class NeuroglyphComponent : ScriptableObject
    {
        public abstract void Activate(GameObject target);
        public abstract void Deactivate(GameObject target);
    }
}