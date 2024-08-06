using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Sprint State",
        menuName = "Character States/Player/Sprint State",
        order = 0
    )]
    public class PlayerSprintState : PlayerGroundedState
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

            if (stateController.InputReader.SprintInput == false)
            {
                stateController.ChangeState("Player Move State");
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterSprint characterSprint))
            {
                characterSprint.Sprint(moveInput.x);
            }
        }
    }
}
