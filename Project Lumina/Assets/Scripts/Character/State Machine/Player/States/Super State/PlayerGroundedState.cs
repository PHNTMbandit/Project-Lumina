using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected Vector2 moveInput;

        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onJump += stateController.Jump;
            stateController.InputReader.onRoll += stateController.Roll;

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.RechargeDash();
            }
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onJump -= stateController.Jump;
            stateController.InputReader.onRoll -= stateController.Roll;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            moveInput = stateController.InputReader.MoveInput;

            if (stateController.CharacterFall.CanFallThrough() && moveInput.y <= -0.8f)
            {
                stateController.CharacterFall.StartCoroutine(
                    stateController.CharacterFall.FallThrough()
                );
            }
        }
    }
}
