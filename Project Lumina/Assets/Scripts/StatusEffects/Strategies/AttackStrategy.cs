using ProjectLumina.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackStrategy : IStatusEffect
{
    [Range(-100, 100), SuffixLabel("%"), SerializeField]
    private float _attackModifier;

    public void Activate(GameObject target)
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

    public void Deactivate(GameObject target)
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