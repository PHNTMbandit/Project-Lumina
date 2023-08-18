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

            stateController.InputReader.onAttack = TryAttack;
            stateController.InputReader.onJump = TryJump;
            stateController.InputReader.onRoll = TryRoll;
            stateController.PlayerAerialAttack.ResetAerialCombo();
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onAttack -= TryAttack;
            stateController.InputReader.onJump -= TryJump;
            stateController.InputReader.onRoll -= TryRoll;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            moveInput = stateController.InputReader.MoveInput.x;

            if (stateController.PlayerFall.IsFalling())
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(moveInput);
        }

        protected void TryAttack()
        {
            stateController.ChangeState(stateController.GetState("Melee Attack"));
        }

        protected void TryJump()
        {
            if (stateController.PlayerJump.CanJump())
            {
                stateController.ChangeState(stateController.GetState("Jump"));
            }
        }

        protected void TryRoll()
        {
            stateController.ChangeState(stateController.GetState("Roll"));
        }
    }
}