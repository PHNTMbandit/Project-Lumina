using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Dash State",
        menuName = "Character States/Player/Dash State",
        order = 0
    )]
    public class PlayerDashState : PlayerInAirState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.UseDash();
            }
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.IsDashing == false)
                {
                    stateController.ChangeState("Player Fall State");
                }
            }
        }
    }
}
