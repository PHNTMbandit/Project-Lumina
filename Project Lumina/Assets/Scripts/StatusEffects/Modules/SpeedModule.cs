using ProjectLumina.Abilities;
using ProjectLumina.StatusEffects.Modules;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpeedModule : Module
{
    [Range(-100, 100), SuffixLabel("%"), SerializeField]
    private float _moveSpeedModifier;

    public override void Activate(GameObject target)
    {
        if (target.TryGetComponent(out CharacterMove characterMove))
        {

        }
    }

    public override void Deactivate(GameObject target)
    {
        if (target.TryGetComponent(out CharacterMove characterMove))
        {

        }
    }
}