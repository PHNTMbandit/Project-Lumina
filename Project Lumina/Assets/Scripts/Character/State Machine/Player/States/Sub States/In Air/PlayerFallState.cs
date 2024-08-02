using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Fall State",
        menuName = "Character States/Player/Fall State",
        order = 0
    )]
    public class PlayerFallState : PlayerInAirState
    {
        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterFall.SetGravityScale();
            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }
    }
}
