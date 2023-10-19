using System.Collections.Generic;
using ProjectLumina.Data.StatusEffects;
using UnityEngine;

namespace ProjectLumina.Character
{
    public class CharacterStatusEffects : MonoBehaviour
    {
        [SerializeField]
        private List<StatusEffect> _statusEffects;

        private void Update()
        {
            if (_statusEffects.Count > 0)
            {
                for (int i = 0; i < _statusEffects.Count; i++)
                {
                    _statusEffects[i].UpdateStatusEffect();
                }
            }
        }

        public void AddStatusEffect(StatusEffect statusEffect)
        {
            if (!_statusEffects.Contains(statusEffect))
            {
                statusEffect.onStatusEffectDeactivated += RemoveStatusEffect;
                _statusEffects.Add(statusEffect);
            }

            _statusEffects.Find(i => i.StatusEffectName == statusEffect.StatusEffectName).ActivateStatusEffect(gameObject);
        }

        public void RemoveStatusEffect(StatusEffect statusEffect)
        {
            _statusEffects.Find(i => i.StatusEffectName == statusEffect.StatusEffectName).DeactivateStatusEffect();

            if (_statusEffects.Contains(statusEffect))
            {
                statusEffect.onStatusEffectDeactivated -= RemoveStatusEffect;
                _statusEffects.Remove(statusEffect);
            }
        }
    }
}