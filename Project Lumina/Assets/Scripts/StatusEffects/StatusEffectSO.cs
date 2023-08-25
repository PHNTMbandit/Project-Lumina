using System;
using ProjectLumina.StatusEffects.Modules;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.StatusEffects
{
    [Serializable]
    public enum StatusEffectType
    {
        Buff,
        Debuff
    }

    [CreateAssetMenu(fileName = "New Status Effect", menuName = "Project Lumina/Status Effect", order = 3)]
    public class StatusEffectSO : ScriptableObject
    {
        [field: SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public StatusEffectType StatusEffectType { get; private set; }

        [field: SerializeField]
        public string StatusEffectName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField, SerializeReference, Space]
        public Module[] Modules { get; private set; }

        public virtual void ApplyStatusEffect(GameObject target)
        {
            foreach (var module in Modules)
            {
                module.Activate(target);
            }
        }

        public virtual void RemoveStatusEffect(GameObject target)
        {
            foreach (var module in Modules)
            {
                module.Deactivate(target);
            }
        }
    }
}