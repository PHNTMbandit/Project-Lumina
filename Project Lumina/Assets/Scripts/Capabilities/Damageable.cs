using ProjectLumina.Data;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(Health))]
    [AddComponentMenu("Capabilities/Damageable")]
    public class Damageable : MonoBehaviour
    {
        public bool IsDamageable { get; private set; } = true;

        private Health _health;

        public UnityEvent onDamaged;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public void Damage(float damage)
        {
            if (IsDamageable)
            {
                if (_health.CurrentHealth > 0)
                {
                    onDamaged?.Invoke();
                }
                else
                {
                    IsDamageable = false;
                }

                _health.ChangeHealth(-damage);
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public void SetDamageable(bool isDamageable)
        {
            IsDamageable = isDamageable;
        }
    }
}