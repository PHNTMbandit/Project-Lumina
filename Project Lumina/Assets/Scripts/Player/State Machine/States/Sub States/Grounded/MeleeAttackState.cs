using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class MeleeAttackState : GroundedState
    {
        public MeleeAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onAttack = TryAttack;

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                characterMeleeAttack.UseMeleeAttack();
                characterMeleeAttack.onComboFinished = ChangeToIdle;
            }
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onAttack -= TryAttack;

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                characterMeleeAttack.onComboFinished -= ChangeToIdle;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(moveInput);
            }
        }
    }
}