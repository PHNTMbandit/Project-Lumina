using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Roll Attack State",
        menuName = "Character States/Player/Roll Attack State",
        order = 0
    )]
    public class PlayerRollAttackState : PlayerGroundedState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.RollAttack();
                stateController.InputReader.onAttack += characterRollAttack.RollAttack;
            }
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                stateController.InputReader.onAttack -= characterRollAttack.RollAttack;
                characterRollAttack.FinishRollAttack();
            }
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (characterRollAttack.IsRollAttacking == false)
                {
                    stateController.ChangeState("Player Idle State");
                }
            }
        }
    }
}
