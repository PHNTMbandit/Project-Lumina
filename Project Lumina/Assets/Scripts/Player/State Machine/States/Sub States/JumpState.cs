using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Jump State", menuName = "Project Lumina/States/Jump State")]
    public class JumpState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerJump.Jump();
        }
    }
}