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
        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                if (characterWallSlide.CanWallSlide == false)
                {
                    stateController.ChangeState("Player Fall State");
                }
            }

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState("Player Idle State");
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
