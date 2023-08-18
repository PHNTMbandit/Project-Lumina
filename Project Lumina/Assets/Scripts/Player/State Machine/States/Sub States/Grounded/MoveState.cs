using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Move State", menuName = "Project Lumina/States/Move State")]
    public class MoveState : GroundedState
    {
        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (moveInput == 0)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(moveInput);
        }
    }
}