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

        [SerializeField]
        private bool _alwaysVisible;

        private void Awake()
        {
            _health.OnHealthChanged.AddListener(UpdateUI);

            UpdateUI();
        }

        private void UpdateUI()
        {
            _slider.maxValue = _health.MaxHealth;
            _slider.value = _health.CurrentHealth;

            if (_text != null)
            {
                _text.SetText($"{_health.CurrentHealth}");
            }

            if (_alwaysVisible == false)
            {
                if (_health.CurrentHealth >= _health.MaxHealth || _health.CurrentHealth <= 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}