using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Roll Attack State", menuName = "Project Lumina/States/Roll Attack State")]
    public class RollAttackState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerRollAttack.RollAttack();
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerRollAttack.IsRollAttacking == false)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }
    }
}