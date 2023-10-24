using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onJump = stateController.TryWallJump;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onJump -= stateController.TryWallJump;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                if (characterWallSlide.CanWallSlide == false)
                {
                    stateController.StateMachine.ChangeState(stateController.FallState);
                }
            }

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                characterWallSlide.Slide();
            }
        }
    }
}