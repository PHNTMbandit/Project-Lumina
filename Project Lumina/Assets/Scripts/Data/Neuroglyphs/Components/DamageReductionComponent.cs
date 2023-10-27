using ProjectLumina.Capabilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(fileName = "New Damage Reduction", menuName = "Project Lumina/Neuroglyphs/Components/Damage Reduction", order = 0)]
    public class DamageReductionComponent : NeuroglyphComponent
    {
        [Range(0, 1000), SuffixLabel("%"), SerializeField]
        private float _damageReduction;

        public override void Activate(GameObject user)
        {
            if (user.TryGetComponent(out Damageable damageable))
            {
                damageable.DamageReduction.SetBaseValue(_damageReduction);
            }
        }

        public override void Deactivate(GameObject user)
        {
            if (user.TryGetComponent(out Damageable damageable))
            {
                damageable.DamageReduction.SetBaseValue(0);
            }
        }

        public override string GetComponentDescription()
        {
            return $"Damage up to {_damageReduction} is nullified";
        }
    }
}