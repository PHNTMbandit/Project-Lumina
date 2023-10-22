using System.Collections.Generic;
using ProjectLumina.Data.StatusEffects;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Status Effects")]
    public class CharacterStatusEffects : MonoBehaviour
    {
        [field: SerializeField]
        public List<StatusEffect> StatusEffects { get; private set; }

        public UnityAction onStatusEffectsChanged;

        private void Update()
        {
            if (StatusEffects.Count > 0)
            {
                for (int i = 0; i < StatusEffects.Count; i++)
                {
                    StatusEffects[i].UpdateStatusEffect();
                }
            }
        }

        public void AddStatusEffect(StatusEffect statusEffect)
        {
            if (!StatusEffects.Contains(statusEffect))
            {
                statusEffect.onStatusEffectTimerEnd += RemoveStatusEffect;
                StatusEffects.Add(statusEffect);

                onStatusEffectsChanged?.Invoke();
            }

            StatusEffects.Find(i => i.StatusEffectName == statusEffect.StatusEffectName).AddStatusEffect(gameObject);
        }

        public void RemoveStatusEffect(StatusEffect statusEffect)
        {
            if (StatusEffects.Contains(statusEffect))
            {
                StatusEffects.Remove(statusEffect);

                onStatusEffectsChanged?.Invoke();
            }
        }
    }
}