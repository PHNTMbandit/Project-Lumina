using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Melee Attack State",
        menuName = "Character States/Player/Melee Attack State",
        order = 0
    )]
    public class PlayerMeleeAttackState : PlayerGroundedState
    {
        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}
