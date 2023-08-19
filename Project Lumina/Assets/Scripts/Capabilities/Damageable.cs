using ProjectLumina.Data;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(Health))]
    [AddComponentMenu("Capabilities/Damageable")]
    public class Damageable : MonoBehaviour
    {
        public UnityEvent onDamage;

        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        public virtual void Damage(float damage)
        {
            _health.ChangeHealth(-damage);

            onDamage?.Invoke();
        }
    }
}