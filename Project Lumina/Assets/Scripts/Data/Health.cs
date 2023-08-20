using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Data
{
    [AddComponentMenu("Data/Health")]
    public class Health : MonoBehaviour
    {
        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value <= 0 ? 0 : value >= _maxHealth ? _maxHealth : value;
        }

        public float MaxHealth => _maxHealth;

        [BoxGroup("Health"), Range(0, 10000), LabelWidth(125), SerializeField]
        private float _maxHealth;

        [BoxGroup("Health"), ProgressBar(0, "_maxHealth", ColorGetter = "GetHealthBarColour"), LabelWidth(125), SerializeField]
        private float _currentHealth;

        [Space]
        public UnityEvent<float, float> OnHealthChanged;

        public UnityEvent onZeroHealth;

        public void ChangeHealth(float amount)
        {
            CurrentHealth += amount;

            OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);

            if (CurrentHealth <= 0)
            {
                onZeroHealth?.Invoke();
            }
        }

        private Color GetHealthBarColour(float value)
        {
            return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / _maxHealth, .5f));
        }
    }
}