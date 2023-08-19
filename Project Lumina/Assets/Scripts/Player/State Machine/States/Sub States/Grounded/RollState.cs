using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Roll State", menuName = "Project Lumina/States/Roll State")]
    public class RollState : GroundedState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.PlayerRoll.RollCharacter();
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.InputReader.AttackInput)
            {
                stateController.ChangeState(stateController.GetState("Roll Attack"));

            }
            else if (stateController.PlayerRoll.IsRolling == false)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }
    }
}