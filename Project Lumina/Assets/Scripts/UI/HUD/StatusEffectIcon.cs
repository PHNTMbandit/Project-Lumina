using ProjectLumina.Data.StatusEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class StatusEffectIcon : MonoBehaviour
    {
        public StatusEffect StatusEffect { get; private set; }

        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _stack, _timer;

        private void Update()
        {
            _stack.SetText(StatusEffect.CurrentStack.ToString());
            _timer.SetText($"{StatusEffect.StackTimer:F2}");
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetStatusEffect(StatusEffect statusEffect)
        {
            StatusEffect = statusEffect;
        }
    }
}