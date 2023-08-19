using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Wall Slide State", menuName = "Project Lumina/States/Wall Slide State")]
    public class WallSlideState : State
    {
        protected StateController stateController;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            this.stateController = stateController;

            stateController.InputReader.onJump = TryWallJump;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onJump -= TryWallJump;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                if (characterWallSlide.CanWallSlide == false)
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                characterWallSlide.Slide();
            }
        }

        protected void TryWallJump()
        {
            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                characterWallSlide.Jump();
            }

            stateController.ChangeState(stateController.GetState("Jump"));
        }
    }
}