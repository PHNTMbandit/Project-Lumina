using System;
using ProjectLumina.StatusEffects.Modules;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.StatusEffects
{
    [CreateAssetMenu(fileName = "New Status Effect", menuName = "Project Lumina/Status Effect", order = 3)]
    public class StatusEffectSO : ScriptableObject
    {
        [field: SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string StatusEffectName { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public StatusEffectType StatusEffectType { get; private set; }

        [field: SerializeField, EnumPaging]
        public StatusEffectLevel StatusEffectLevel { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField, SerializeReference, Space]
        public Module[][] Modules { get; private set; }

        private void OnEnable()
        {
            Module[] level1Modules = new Module[0];
            Module[] level2Modules = new Module[0];
            Module[] level3Modules = new Module[0];
            Module[] level4Modules = new Module[0];
            Module[] level5Modules = new Module[0];
            Module[] level6Modules = new Module[0];
            Module[] level7Modules = new Module[0];
            Module[] level8Modules = new Module[0];
            Module[] level9Modules = new Module[0];

            Modules = new Module[][]
                              {
                level1Modules,
                level2Modules,
                level3Modules,
                level4Modules,
                level5Modules,
                level6Modules,
                level7Modules,
                level8Modules,
                level9Modules,
                              };
        }

        public virtual void ApplyStatusEffect(GameObject target)
        {

        }

        public virtual void RemoveStatusEffect(GameObject target)
        {
        }
    }
}