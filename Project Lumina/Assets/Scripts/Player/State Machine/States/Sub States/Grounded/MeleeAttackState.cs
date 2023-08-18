using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Melee Attack State", menuName = "Project Lumina/States/Melee Attack State")]
    public class MeleeAttackState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerMeleeAttack.UseCombo();
            stateController.PlayerMeleeAttack.onComboFinished = ChangeToIdle;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.PlayerMeleeAttack.onComboFinished -= ChangeToIdle;
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(moveInput);
        }

        private void ChangeToIdle()
        {
            stateController.ChangeState(stateController.GetState("Idle"));
        }
    }
}