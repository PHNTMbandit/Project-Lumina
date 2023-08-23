using ProjectLumina.Data;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(Health))]
    [AddComponentMenu("Capabilities/Damageable")]
    public class Damageable : MonoBehaviour
    {
        private Health _health;

        public UnityEvent onDamaged;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public virtual void Damage(float damage)
        {
            _health.ChangeHealth(-damage);

            if (_health.CurrentHealth > 0)
            {
                onDamaged?.Invoke();
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}