using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Fall State", menuName = "Project Lumina/States/Fall State")]
    public class FallState : InAirState
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