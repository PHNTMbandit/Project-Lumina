using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Wall Slide State",
        menuName = "Character States/Player/Wall Slide State",
        order = 0
    )]
    public class PlayerWallSlideState : PlayerState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onJump += stateController.WallJump;
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onJump -= stateController.WallJump;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (
                stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide)
                && stateController.HasCharacterAbility(out CharacterJump characterJump)
            )
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState("Player Idle State");
                }
                else if (characterWallSlide.CanWallSlide() == false)
                {
                    stateController.ChangeState("Player Fall State");
                }
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                characterWallSlide.Slide();
            }
        }
    }
}
