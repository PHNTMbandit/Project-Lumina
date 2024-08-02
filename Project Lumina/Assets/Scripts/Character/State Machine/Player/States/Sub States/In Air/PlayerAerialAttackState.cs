using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Aerial Attack State",
        menuName = "Character States/Player/Aerial Attack State",
        order = 0
    )]
    public class PlayerAerialAttackState : PlayerInAirState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            if (
                stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack)
            )
            {
                characterAerialAttack.AerialAttack();
                stateController.InputReader.onAttack += characterAerialAttack.AerialAttack;
            }
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            if (
                stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack)
            )
            {
                stateController.InputReader.onAttack -= characterAerialAttack.AerialAttack;
                characterAerialAttack.FinishAerialAttack();
            }
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState("Player Idle State");
                }
            }

            if (
                stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack)
            )
            {
                if (characterAerialAttack.IsAttacking == false)
                {
                    stateController.ChangeState("Player Fall State");
                }
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);

            if (
                stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack)
            )
            {
                if (characterAerialAttack.IsSlowStop == false)
                {
                    stateController.CharacterFall.SetGravityScale();
                }
            }
        }
    }
}
