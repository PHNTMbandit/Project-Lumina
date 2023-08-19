using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Jump State", menuName = "Project Lumina/States/Jump State")]
    public class JumpState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerJump.Jump();
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

            if (stateController.PlayerFall.IsFalling())
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerJump.SetGravityScale();
            stateController.PlayerMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }
    }
}