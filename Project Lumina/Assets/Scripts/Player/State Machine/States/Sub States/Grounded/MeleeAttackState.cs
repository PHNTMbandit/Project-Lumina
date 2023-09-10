using ProjectLumina.Character;
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

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                characterMeleeAttack.MeleeAttack();
                stateController.InputReader.onAttack += characterMeleeAttack.MeleeAttack;
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                stateController.InputReader.onAttack -= characterMeleeAttack.MeleeAttack;
                characterMeleeAttack.FinishMeleeAttack();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                if (characterMeleeAttack.IsAttacking == false)
                {
                    ChangeToIdle();
                }
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