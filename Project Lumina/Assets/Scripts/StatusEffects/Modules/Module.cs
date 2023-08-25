using System;
using UnityEngine;

namespace ProjectLumina.StatusEffects.Modules
{
    [Serializable]
    public abstract class Module
    {
        public abstract void Activate(GameObject target);

        public abstract void Deactivate(GameObject target);
    }
}