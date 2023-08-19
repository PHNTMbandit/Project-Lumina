using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Aerial Melee Attack State", menuName = "Project Lumina/States/Aerial Attack State")]
    public class AerialAttackState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerAerialAttack.UseAerialAttack();
            stateController.PlayerAerialAttack.onComboFinished = ChangeToFall;
            stateController.InputReader.onAttack = stateController.PlayerAerialAttack.UseAerialAttack;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.PlayerAerialAttack.onComboFinished -= ChangeToFall;
            stateController.InputReader.onAttack -= stateController.PlayerAerialAttack.UseAerialAttack;
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

            stateController.PlayerAerialAttack.SetGravityScale();
            stateController.PlayerMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }

        private void ChangeToFall()
        {
            stateController.ChangeState(stateController.GetState("Fall"));
        }
    }
}