using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Jump State",
        menuName = "Character States/Player/Jump State",
        order = 0
    )]
    public class PlayerJumpState : PlayerInAirState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onAttack += stateController.AerialAttack;

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.Jump();
            }
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onAttack -= stateController.AerialAttack;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.CharacterFall.IsFalling())
            {
                stateController.ChangeState("Player Fall State");
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.SetGravityScale();
            }
        }
    }
}
