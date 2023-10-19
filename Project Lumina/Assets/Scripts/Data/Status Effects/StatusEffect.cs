using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Data.StatusEffects
{
    [CreateAssetMenu(fileName = "New Status Effect", menuName = "Project Lumina/Status Effects/Status Effect", order = 0)]
    public class StatusEffect : ScriptableObject
    {
        [field: TabGroup("Details"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite Icon { get; private set; }

        [field: TabGroup("Details"), SerializeField]
        public string StatusEffectName { get; private set; }

        [field: TabGroup("Details"), TextArea, SerializeField]
        public string Description { get; private set; }

        [TabGroup("Components"), ShowInInspector, SerializeField]
        private StatusEffectComponent[] _components;

        public UnityAction<StatusEffect> onStatusEffectDeactivated;

        public void ActivateStatusEffect(GameObject target)
        {
            for (int i = 0; i < _components.Length; i++)
            {
                _components[i].ApplyEffect(target);
            }
        }

        public void UpdateStatusEffect()
        {
            for (int i = 0; i < _components.Length; i++)
            {
                _components[i].UpdateEffect();
            }

            if (_components.All(i => i.IsRunning() == false))
            {
                DeactivateStatusEffect();
            }
        }

        public void DeactivateStatusEffect()
        {
            for (int i = 0; i < _components.Length; i++)
            {
                _components[i].RemoveEffect();
            }

            onStatusEffectDeactivated?.Invoke(this);
        }
    }
}