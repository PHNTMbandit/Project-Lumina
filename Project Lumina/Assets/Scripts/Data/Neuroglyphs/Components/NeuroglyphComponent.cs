using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    public abstract class NeuroglyphComponent : ScriptableObject
    {
        public abstract void Activate(GameObject user);
        public abstract void Deactivate(GameObject user);
        public abstract string GetComponentDescription();
    }
}