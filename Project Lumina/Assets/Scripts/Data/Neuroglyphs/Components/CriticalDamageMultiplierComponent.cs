using ProjectLumina.Character;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(
        fileName = "New Critical Damage Multiplier Component",
        menuName = "Project Lumina/Neuroglyphs/Components/Critical Damage Multiplier",
        order = 0
    )]
    public class CriticalDamageMultiplierComponent : NeuroglyphComponent
    {
        [Range(0, 1000), SuffixLabel("%"), SerializeField]
        private float _criticalDamageMultiplierModifier;

        public override void Activate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterAerialAttack aerialAttack))
            {
                foreach (var attack in aerialAttack.Attacks)
                {
                    attack.CriticalDamageMultiplier.AddModifier(
                        new PercentageStatModifier(_criticalDamageMultiplierModifier)
                    );
                }
            }

            if (user.TryGetComponent(out CharacterFallAttack fallAttack))
            {
                fallAttack.Attack.CriticalDamageMultiplier.AddModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }

            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                foreach (var attack in meleeAttack.Attacks)
                {
                    attack.CriticalDamageMultiplier.AddModifier(
                        new PercentageStatModifier(_criticalDamageMultiplierModifier)
                    );
                }
            }

            if (user.TryGetComponent(out CharacterRollAttack rollAttack))
            {
                rollAttack.Attack.CriticalDamageMultiplier.AddModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }

            if (user.TryGetComponent(out CharacterShoot shoot))
            {
                shoot.Attack.CriticalDamageMultiplier.AddModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }
        }

        public override void Deactivate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterAerialAttack aerialAttack))
            {
                foreach (var attack in aerialAttack.Attacks)
                {
                    attack.CriticalDamageMultiplier.RemoveModifier(
                        new PercentageStatModifier(_criticalDamageMultiplierModifier)
                    );
                }
            }

            if (user.TryGetComponent(out CharacterFallAttack fallAttack))
            {
                fallAttack.Attack.CriticalDamageMultiplier.RemoveModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }

            if (user.TryGetComponent(out CharacterMeleeAttack meleeAttack))
            {
                foreach (var attack in meleeAttack.Attacks)
                {
                    attack.CriticalDamageMultiplier.RemoveModifier(
                        new PercentageStatModifier(_criticalDamageMultiplierModifier)
                    );
                }
            }

            if (user.TryGetComponent(out CharacterRollAttack rollAttack))
            {
                rollAttack.Attack.CriticalDamageMultiplier.RemoveModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }

            if (user.TryGetComponent(out CharacterShoot shoot))
            {
                shoot.Attack.CriticalDamageMultiplier.RemoveModifier(
                    new PercentageStatModifier(_criticalDamageMultiplierModifier)
                );
            }
        }

        public override string GetComponentDescription()
        {
            return $"Critical damage multiplier is increased by {_criticalDamageMultiplierModifier}%";
        }
    }
}
