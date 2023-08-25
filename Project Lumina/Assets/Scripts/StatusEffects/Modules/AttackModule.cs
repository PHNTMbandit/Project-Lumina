using ProjectLumina.Abilities;
using ProjectLumina.StatusEffects.Modules;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackModule : Module
{
    [Range(-100, 100), SuffixLabel("%"), SerializeField]
    private float _attackModifier;

    public override void Activate(GameObject target)
    {
        if (target.TryGetComponent(out CharacterAerialAttack aerialAttack))
        {

        }

        if (target.TryGetComponent(out CharacterFallAttack fallAttack))
        {

        }

        if (target.TryGetComponent(out CharacterMeleeAttack meleeAttack))
        {

        }

        if (target.TryGetComponent(out CharacterRollAttack rollAttack))
        {

        }
    }

    public override void Deactivate(GameObject target)
    {
        if (target.TryGetComponent(out CharacterAerialAttack aerialAttack))
        {

        }

        if (target.TryGetComponent(out CharacterFallAttack fallAttack))
        {

        }

        if (target.TryGetComponent(out CharacterMeleeAttack meleeAttack))
        {

        }

        if (target.TryGetComponent(out CharacterRollAttack rollAttack))
        {

        }
    }
}