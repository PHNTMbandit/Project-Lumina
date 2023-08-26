using System.Collections.Generic;
using ProjectLumina.StatusEffects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Status Effects")]
    public class CharacterStatusEffects : MonoBehaviour
    {
        [SerializeField]
        private List<StatusEffectSO> _activeStatusEffects;

        private void Start()
        {
            UpdateStatusEffects();
        }

        public void AddStatusEffect(StatusEffectSO statusEffect)
        {
            if (!_activeStatusEffects.Contains(statusEffect))
            {
                _activeStatusEffects.Add(statusEffect);

                statusEffect.Apply(gameObject);
            }
        }

        public void RemoveStatusEffect(StatusEffectSO statusEffect)
        {
            if (_activeStatusEffects.Contains(statusEffect))
            {
                _activeStatusEffects.Remove(statusEffect);

                statusEffect.Revert(gameObject);
            }
        }

        public void UpdateStatusEffects()
        {
            foreach (StatusEffectSO statusEffect in _activeStatusEffects)
            {
                statusEffect.Revert(gameObject);
                statusEffect.Apply(gameObject);
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
    }
}