using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Fall State", menuName = "Project Lumina/States/Fall State")]
    public class FallState : InAirState
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

            if (stateController.PlayerJump.IsGrounded)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerFall.SetGravityScale();
            stateController.PlayerMove.Move(stateController.InputReader.MoveInput.x);
        }
    }
}