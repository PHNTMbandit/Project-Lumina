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

            stateController.InputReader.onDash = TryDash;
            stateController.PlayerMeleeAttack.ResetCombo();
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onDash -= TryDash;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerWallSlide.CanWallSlide && stateController.InputReader.MoveInput.x != 0)
            {
                stateController.ChangeState(stateController.GetState("Wall Slide"));
            }
        }

        protected void TryAttack()
        {
            if (stateController.PlayerAerialAttack.AerialAttackCharge)
            {
                stateController.ChangeState(stateController.GetState("Aerial Attack"));
            }
        }

        protected void TryDash()
        {
            if (stateController.PlayerDash.CurrentDashCharges > 0)
            {
                stateController.ChangeState(stateController.GetState("Dash"));
            }
        }
    }
}