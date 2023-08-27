using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class GroundedState : State
    {
        protected float moveInput;

        public GroundedState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onJump = TryJump;
            stateController.InputReader.onRoll = TryRoll;

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.ResetAerialAttackCombo();
            }

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.ResetDash();
            }
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onJump -= TryJump;
            stateController.InputReader.onRoll -= TryRoll;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            moveInput = stateController.InputReader.MoveInput.x;

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                if (characterFall.IsFalling())
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }
        }

        protected void TryAttack()
        {
            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                stateController.ChangeState(stateController.GetState("Melee Attack"));

            }
        }

        protected void TryJump()
        {
            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.CanJump())
                {
                    stateController.ChangeState(stateController.GetState("Jump"));
                }
            }
        }

        protected void TryRoll()
        {
            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                stateController.ChangeState(stateController.GetState("Roll"));
            }
        }

        protected void ChangeToIdle()
        {
            stateController.ChangeState(stateController.GetState("Idle"));
        }
    }
}