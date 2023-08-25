using System.Collections.Generic;
using ProjectLumina.StatusEffects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Status Effects")]
    public class CharacterStatusEffects : MonoBehaviour
    {
        [TableList, SerializeField]
        private List<StatusEffectSO> _activeStatusEffects;

        public void AddStatusEffect(StatusEffectSO statusEffect)
        {
            if (!_activeStatusEffects.Contains(statusEffect))
            {
                _activeStatusEffects.Add(statusEffect);

                statusEffect.ApplyStatusEffect(gameObject);
            }
        }

        public void RemoveStatusEffect(StatusEffectSO statusEffect)
        {
            if (_activeStatusEffects.Contains(statusEffect))
            {
                _activeStatusEffects.Remove(statusEffect);

                statusEffect.RemoveStatusEffect(gameObject);
            }
        }

        public StatusEffectSO GetStatusEffect(string statusEffectName)
        {
            return _activeStatusEffects.Find(i => i.StatusEffectName == statusEffectName);
        }

        public StatusEffectSO GetStatusEffect(StatusEffectSO statusEffect)
        {
            return _activeStatusEffects.Find(i => i == statusEffect);
        }

        public StatusEffectSO[] GetStatusEffects(StatusEffectType statusEffectType)
        {
            return _activeStatusEffects.FindAll(i => i.StatusEffectType == statusEffectType).ToArray();
        }
    }
}