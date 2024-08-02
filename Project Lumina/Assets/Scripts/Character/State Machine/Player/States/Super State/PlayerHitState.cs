using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Hit State",
        menuName = "Character States/Player/Hit State",
        order = 0
    )]
    public class PlayerHitState : PlayerState
    {
        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            stateController.CharacterMove.StopCharacter();
        }
    }
}
