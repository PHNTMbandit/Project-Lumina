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
