using ProjectLumina.Capabilities;
using ProjectLumina.Data.StatusEffects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data.Components
{
    [CreateAssetMenu(fileName = "DOT Component", menuName = "Project Lumina/Status Effects/Components/DOT", order = 0)]
    public class DOTComponent : StatusEffectComponent
    {
        public int CurrentStack
        {
            get => _currentStacks;
            set => _currentStacks = value <= 0 ? 0 : value >= _maxStacks ? _maxStacks : value;
        }

        public float Damage
        {
            get => _damageStat.Value;
            set => _damageStat.SetBaseValue(value);
        }

        [BoxGroup("Stats"), Range(0, 10), SerializeField]
        private float _damage, duration;

        [BoxGroup("Ticks"), Range(0, 10), SerializeField]
        private int _maxStacks = 10;

        [BoxGroup("Ticks"), Range(0, 10), SerializeField]
        private float _tickInterval = 1.0f;

        private Damageable _target;
        private int _currentStacks;
        private float _stackTimer, _timeSinceLastTick;
        private readonly Stat _damageStat = new(0);

        public override void ApplyEffect(GameObject target)
        {
            CurrentStack++;
            _timeSinceLastTick = 0;
            _stackTimer = duration;
            Damage *= _currentStacks;

            if (target.TryGetComponent(out Damageable damageable))
            {
                _target = damageable;
            }
        }

        public override void UpdateEffect()
        {
            _timeSinceLastTick += Time.deltaTime;

            if (_timeSinceLastTick >= _tickInterval)
            {
                _timeSinceLastTick = 0;

                _target.Damage(Damage);
            }

            _stackTimer -= Time.deltaTime;

            if (_stackTimer <= 0)
            {
                if (CurrentStack > 0)
                {
                    CurrentStack--;
                    Damage /= CurrentStack + 1;
                    _stackTimer = duration;
                }
                else
                {
                    RemoveEffect();
                }
            }
        }

        public override void RemoveEffect()
        {
            CurrentStack = 0;
        }

        public override bool IsRunning()
        {
            return CurrentStack > 0;
        }
    }
}