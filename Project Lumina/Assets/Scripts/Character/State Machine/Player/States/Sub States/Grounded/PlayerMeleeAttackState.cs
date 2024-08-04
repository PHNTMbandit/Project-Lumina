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
        public float cooldown,
            duration = 0.5f;

        private float _enterTime,
            _elapsedTime;
        private PlayerStateController _stateController;

        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            _enterTime = Time.time;
            _stateController = stateController;
            _stateController.InputReader.onAttack += TryNextAttack;
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            _stateController.InputReader.onAttack -= TryNextAttack;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                if (stateController.HasCharacterAbility(out CharacterMeleeAttack meleeAttack))
                {
                    meleeAttack.EndCombo();
                    stateController.ChangeState("Player Idle State");
                }
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }

        private void TryNextAttack()
        {
            if (_elapsedTime >= cooldown)
            {
                _stateController.MeleeAttack();
            }
        }
    }
}
