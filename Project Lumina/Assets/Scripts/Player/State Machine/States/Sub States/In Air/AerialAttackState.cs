using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Aerial Melee Attack State", menuName = "Project Lumina/States/Aerial Attack State")]
    public class AerialAttackState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerAerialAttack.UseAerialCombo();
            stateController.PlayerAerialAttack.onComboFinished = ChangeToFall;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.PlayerAerialAttack.onComboFinished -= ChangeToFall;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerJump.IsGrounded)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }

        private void ChangeToFall()
        {
            stateController.ChangeState(stateController.GetState("Fall"));
        }
    }
}