using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Fall Attack State", menuName = "Project Lumina/States/Fall Attack State")]
    public class FallAttackState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerFallAttack.onFallAttackFinished = ChangeToFall;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.PlayerAerialAttack.ResetAerialCombo();
            stateController.PlayerFallAttack.onFallAttackFinished -= ChangeToFall;
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

            stateController.PlayerFallAttack.SetGravityScale();
            stateController.PlayerMove.Move(stateController.InputReader.MoveInput.x);
        }

        private void ChangeToFall()
        {
            stateController.ChangeState(stateController.GetState("Fall"));
        }
    }
}