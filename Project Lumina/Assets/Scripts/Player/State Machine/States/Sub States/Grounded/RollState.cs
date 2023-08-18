using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Roll State", menuName = "Project Lumina/States/Roll State")]
    public class RollState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerRoll.Roll();
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerRoll.IsRolling == false)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }
    }
}