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
        public bool IsDamageable { get; private set; } = true;

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

        public void HitStop(float duration)
        {
            if (IsDamageable)
            {
                _hitStoppable.Stop(duration);
            }
        }

        public void ShowDamageIndicator(float damage, Vector2 origin)
        {
            if (IsDamageable)
            {
                ObjectPoolController.Instance.GetPooledObject("Damage Indicator", transform.position, ObjectPoolController.Instance.transform, true)
                                             .GetComponent<DamageIndicator>()
                                             .ShowIndicator(damage.ToString(), origin, transform.position);
            }
        }

        public void ShowHitFX(string FXName)
        {
            if (IsDamageable)
            {
                ObjectPoolController.Instance.GetPooledObject(FXName, transform.position, new Quaternion(0, transform.localScale.x, 0, 0), true);
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