using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Idle State",
        menuName = "Character States/Player/Idle State",
        order = 0
    )]
    public class PlayerIdleState : PlayerGroundedState
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

            if (moveInput.x != 0)
            {
                if (stateController.InputReader.SprintInput)
                {
                    stateController.ChangeState("Player Sprint State");
                }
                else
                {
                    stateController.ChangeState("Player Move State");
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
