using System.Collections.Generic;
using ProjectLumina.Abilities;
using ProjectLumina.StatusEffects;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class StatusEffectList : MonoBehaviour
    {
        [SerializeField]
        private CharacterStatusEffects _characterStatusEffects;

        [SerializeField]
        private StatusEffectIcon _templateStatusEffectIcon;

        [SerializeField]
        private Transform _buffTransform, _debuffTransform;

        private readonly List<StatusEffectIcon> _icons = new();

        private void Awake()
        {
            _templateStatusEffectIcon.gameObject.SetActive(false);
        }

        public void GenerateList()
        {
            ResetList();

            foreach (StatusEffectSO statusEffect in _characterStatusEffects.GetStatusEffectsByType(StatusEffectType.Buff))
            {
                StatusEffectIcon statusEffectIcon = Instantiate(_templateStatusEffectIcon.gameObject, _buffTransform).GetComponent<StatusEffectIcon>();
                statusEffectIcon.gameObject.SetActive(true);
                statusEffectIcon.SetIcon(statusEffect.Icon);

                _icons.Add(statusEffectIcon);
            }

            foreach (StatusEffectSO statusEffect in _characterStatusEffects.GetStatusEffectsByType(StatusEffectType.Debuff))
            {
                StatusEffectIcon statusEffectIcon = Instantiate(_templateStatusEffectIcon.gameObject, _debuffTransform).GetComponent<StatusEffectIcon>();
                statusEffectIcon.gameObject.SetActive(true);
                statusEffectIcon.SetIcon(statusEffect.Icon);

                _icons.Add(statusEffectIcon);
            }
        }

        private void ResetList()
        {
            if (_icons.Count > 0)
            {
                foreach (StatusEffectIcon statusEffectIcon in _icons)
                {
                    Destroy(statusEffectIcon.gameObject);
                }

                _icons.Clear();
            }
        }
    }
}