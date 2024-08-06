using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Move State",
        menuName = "Character States/Player/Move State",
        order = 0
    )]
    public class PlayerMoveState : PlayerGroundedState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onAttack += stateController.MeleeAttack;
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onAttack -= stateController.MeleeAttack;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (moveInput.x == 0)
            {
                stateController.ChangeState("Player Idle State");
            }

            if (stateController.HasCharacterAbility(out CharacterSprint characterSprint))
            {
                if (stateController.InputReader.SprintInput)
                {
                    stateController.ChangeState("Player Sprint State");
                }
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}
