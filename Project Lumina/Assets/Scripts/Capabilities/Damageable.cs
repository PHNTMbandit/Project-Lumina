using ProjectLumina.Controllers;
using ProjectLumina.Data;
using ProjectLumina.UI;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(HitStoppable))]
    [AddComponentMenu("Capabilities/Damageable")]
    public class Damageable : MonoBehaviour
    {
        private Health _health;
        private HitStoppable _hitStoppable;

        public UnityEvent onDamaged;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _hitStoppable = GetComponent<HitStoppable>();
        }

        public void Damage(float damage)
        {
            _health.ChangeHealth(-damage);

            if (_health.CurrentHealth > 0)
            {
                onDamaged?.Invoke();
            }
        }

        public void HitStop(float duration)
        {
            if (_health.CurrentHealth > 0)
            {
                _hitStoppable.Stop(duration);
            }
        }

        public void ShowDamageIndicator(float damage, Vector2 origin)
        {
            if (_health.CurrentHealth > 0)
            {
                ObjectPoolController.Instance.GetPooledObject("Damage Indicator", transform.position, ObjectPoolController.Instance.transform, true)
                                             .GetComponent<DamageIndicator>()
                                             .ShowIndicator(damage.ToString(), origin, transform.position);
            }
        }

        public void ShowHitFX(string FXName)
        {
            if (_health.CurrentHealth > 0)
            {
                ObjectPoolController.Instance.GetPooledObject(FXName, transform.position, new Quaternion(0, transform.localScale.x, 0, 0), true);
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}