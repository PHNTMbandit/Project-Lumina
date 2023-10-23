using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected Vector2 moveInput;

        public PlayerGroundedState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onJump = stateController.TryJump;
            stateController.InputReader.onRoll = stateController.TryRoll;

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.ResetDash();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onJump -= stateController.TryJump;
            stateController.InputReader.onRoll -= stateController.TryRoll;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            moveInput = stateController.InputReader.MoveInput;

            if (stateController.CharacterFall.IsFalling())
            {
                stateController.StateMachine.ChangeState(stateController.FallState);
            }
            else if (stateController.CharacterFall.CanFallThrough() && moveInput.y <= -0.8f)
            {
                stateController.CharacterFall.StartCoroutine(stateController.CharacterFall.FallThrough());
            }
        }
    }
}