using ProjectLumina.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Health _health;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _health.OnHealthChanged.AddListener(UpdateUI);

            UpdateUI();
        }

        private void UpdateUI()
        {
            _slider.maxValue = _health.MaxHealth;
            _slider.value = _health.CurrentHealth;

            _text.SetText($"{_health.CurrentHealth}");
        }
    }
}