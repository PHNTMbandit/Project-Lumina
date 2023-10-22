using System.Collections.Generic;
using ProjectLumina.Character;
using ProjectLumina.Data.StatusEffects;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class StatusEffectList : MonoBehaviour
    {
        [SerializeField]
        private CharacterStatusEffects _characterStatusEffects;

        [SerializeField]
        private StatusEffectIcon _templateIcon;

        [SerializeField]
        private Transform _transform;

        private readonly List<StatusEffectIcon> _icons = new();

        private void Awake()
        {
            _templateIcon.gameObject.SetActive(false);

            _characterStatusEffects.onStatusEffectsChanged += GenerateList;
        }

        public void GenerateList()
        {
            ResetList();

            foreach (StatusEffect statusEffect in _characterStatusEffects.StatusEffects)
            {
                StatusEffectIcon icon = Instantiate(_templateIcon.gameObject, _transform).GetComponent<StatusEffectIcon>();
                icon.gameObject.SetActive(true);

                icon.SetStatusEffect(statusEffect);
                icon.SetIcon(statusEffect.Icon);

                _icons.Add(icon);
            }
        }

        private void ResetList()
        {
            if (_icons.Count > 0)
            {
                foreach (StatusEffectIcon icon in _icons)
                {
                    Destroy(icon.gameObject);
                }

                _icons.Clear();
            }
        }
    }
}