using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Roll State",
        menuName = "Character States/Player/Roll State",
        order = 0
    )]
    public class PlayerRollState : PlayerGroundedState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onAttack += stateController.RollAttack;

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                characterRoll.RollCharacter();
            }
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onAttack -= stateController.RollAttack;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                if (!characterRoll.IsRolling)
                {
                    stateController.ChangeState("Player Idle State");
                }
            }
        }
    }
}
