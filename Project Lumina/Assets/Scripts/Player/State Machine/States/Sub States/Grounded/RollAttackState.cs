using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class RollAttackState : GroundedState
    {
        public RollAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.UseRollAttack();
                characterRollAttack.onRollAttackFinished = ChangeToIdle;
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.onRollAttackFinished -= ChangeToIdle;
            }
        }
    }
}