using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Roll Attack State", menuName = "Project Lumina/States/Roll Attack State")]
    public class RollAttackState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.UseRollAttack();
                characterRollAttack.onRollAttackFinished = ChangeToIdle;
            }
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.onRollAttackFinished -= ChangeToIdle;
            }
        }
    }
}