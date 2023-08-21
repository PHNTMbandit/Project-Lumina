using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class GroundedState : State
    {
        protected float moveInput;
        protected StateController stateController;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            this.stateController = stateController;

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

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onJump -= TryJump;
            stateController.InputReader.onRoll -= TryRoll;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

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
            stateController.ChangeState(stateController.GetState("Melee Attack"));
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
            stateController.ChangeState(stateController.GetState("Roll"));
        }

        protected void ChangeToIdle()
        {
            stateController.ChangeState(stateController.GetState("Idle"));
        }
    }
}