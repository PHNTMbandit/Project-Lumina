using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Dash State", menuName = "Project Lumina/States/Dash State")]
    public class DashState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerDash.UseDash();
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerDash.IsDashing == false)
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
        }
    }
}