using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Sprint State", menuName = "Project Lumina/States/Sprint State")]
    public class SprintState : GroundedState
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

            if (moveInput == 0)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }

            if (stateController.InputReader.SprintInput == false)
            {
                stateController.ChangeState(stateController.GetState("Move"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerSprint.Sprint(moveInput);
        }
    }
}