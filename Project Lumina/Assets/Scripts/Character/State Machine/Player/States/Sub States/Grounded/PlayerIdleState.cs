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
        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}
