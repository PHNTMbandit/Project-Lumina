using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class InAirState : State
    {
        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerJump.IsGrounded)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }
    }
}