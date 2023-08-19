using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "Project Lumina/States/Idle State")]
    public class IdleState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.InputReader.onAttack = TryAttack;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onAttack -= TryAttack;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (moveInput != 0)
            {
                stateController.ChangeState(stateController.GetState("Move"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.MoveCharacter(moveInput);
        }
    }
}