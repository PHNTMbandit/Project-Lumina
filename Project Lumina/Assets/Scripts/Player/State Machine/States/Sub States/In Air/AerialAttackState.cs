using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class AerialAttackState : InAirState
    {
        public AerialAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onAttack = TryAttack;

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.UseAerialAttack();
                characterAerialAttack.onComboFinished = ChangeToFall;
            }
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onAttack -= TryAttack;

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.onComboFinished -= ChangeToFall;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
                {
                    if (characterAerialAttack.IsSlowStop == false)
                    {
                        characterFall.SetGravityScale();
                    }
                }
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}