using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data.StatusEffects
{
    [CreateAssetMenu(fileName = "New Electric Status Effect", menuName = "Project Lumina/Status Effects/Electric", order = 0)]
    public class ElectricStatusEffect : StatusEffect
    {
        [TabGroup("Stats"), Range(0, 10), SerializeField]
        private float _tickInterval = 1.0f;

        private float _timeSinceLastTick;
        private Vector3 _previousPosition;

        public override void AddStatusEffect(GameObject target)
        {
            base.AddStatusEffect(target);

            _timeSinceLastTick = 0;
            _previousPosition = target.transform.position;
        }

        public override void UpdateStatusEffect()
        {
            base.UpdateStatusEffect();

            _timeSinceLastTick += Time.deltaTime;

            if (target != null)
            {
                Vector3 deltaPosition = target.transform.position - _previousPosition;
                float speed = deltaPosition.magnitude / Time.deltaTime;

                if (speed > 0.1f)
                {
                    if (_timeSinceLastTick >= _tickInterval)
                    {
                        _timeSinceLastTick = 0;

                        DealElectricDamage();
                    }
                }

                _previousPosition = target.transform.position;
            }
        }

        private void DealElectricDamage()
        {
            target.Damage(damage.Value);

            if (target.TryGetComponent(out DamageIndicator damageIndicator))
            {
                damageIndicator.ShowDamageIndicator(false, damage.Value, target.transform.position, Colour);
            }
        }
    }
}