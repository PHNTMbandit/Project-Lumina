using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Dash State", menuName = "Project Lumina/States/Dash State")]
    public class DashState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.UseDash();
            }
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.IsDashing == false)
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }
        }
    }
}