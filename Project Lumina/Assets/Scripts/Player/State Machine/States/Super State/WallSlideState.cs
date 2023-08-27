using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class WallSlideState : State
    {
        public WallSlideState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onJump = TryWallJump;
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onJump -= TryWallJump;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

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

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

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