using ProjectLumina.Capabilities;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Data.StatusEffects
{
    public abstract class StatusEffect : ScriptableObject
    {
        public float StackTimer { get; private set; }

        public int CurrentStack
        {
            get => _currentStacks;
            set => _currentStacks = value <= 0 ? 0 : value >= _maxStacks ? _maxStacks : value;
        }

        [field: TabGroup("Details"), PreviewField(Alignment = ObjectFieldAlignment.Left), SerializeField]
        public Sprite Icon { get; private set; }

        [field: TabGroup("Details"), SerializeField]
        public string StatusEffectName { get; private set; }

        [field: TabGroup("Details"), TextArea, SerializeField]
        public string Description { get; private set; }

        [TabGroup("Stats"), Range(0, 10), SerializeField]
        private float _damage, _duration;

        [TabGroup("Stats"), Range(0, 10), SerializeField]
        private int _maxStacks = 10;

        [TabGroup("Stats"), Range(0, 10), SerializeField]
        private float _tickInterval = 1.0f;

        [field: TabGroup("UI"), ColorPalette, SerializeField]
        public Color Colour { get; private set; }

        [field: TabGroup("UI"), SerializeField]
        public GameObject Indicator { get; private set; }

        protected Stat damage;
        protected Damageable target;
        private int _currentStacks = 0;
        private float _timeSinceLastTick;

        public UnityAction<StatusEffect> onStatusEffectTimerEnd;

        protected abstract void ActivateStatusEffect();

        public virtual void AddStatusEffect(GameObject target)
        {
            CurrentStack++;
            _timeSinceLastTick = 0;
            StackTimer = _duration;

            if (target.TryGetComponent(out Damageable damageable))
            {
                this.target = damageable;
            }

            if (target.TryGetComponent(out StatusEffectIndicator statusEffectIndicator))
            {
                statusEffectIndicator.ShowStatusEffectIndicator(Indicator.name);
            }
        }

        public virtual void UpdateStatusEffect()
        {
            _timeSinceLastTick += Time.deltaTime;
            damage = new(_damage * CurrentStack);

            if (_timeSinceLastTick >= _tickInterval)
            {
                _timeSinceLastTick = 0;

                ActivateStatusEffect();
            }

            StackTimer -= Time.deltaTime;

            if (StackTimer <= 0)
            {
                CurrentStack--;
                StackTimer = _duration;

                if (CurrentStack <= 0)
                {
                    onStatusEffectTimerEnd?.Invoke(this);
                }
            }
        }
    }
}