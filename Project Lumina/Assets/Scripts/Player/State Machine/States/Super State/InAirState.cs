using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class InAirState : State
    {
        protected StateController stateController;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            this.stateController = stateController;

            stateController.PlayerMeleeAttack.ResetCombo();
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerWallSlide.CanWallSlide && stateController.InputReader.MoveInput.x != 0)
            {
                stateController.ChangeState(stateController.GetState("Wall Slide"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(stateController.InputReader.MoveInput.x);
        }

        protected void TryAttack()
        {
            if (stateController.PlayerAerialAttack.AerialAttackCharge)
            {
                stateController.ChangeState(stateController.GetState("Aerial Attack"));
            }
        }
    }
}