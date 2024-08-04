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
                if (stateController.HasCharacterAbility(out CharacterAerialAttack aerialAttack))
                {
                    aerialAttack.EndCombo();
                    stateController.ChangeState("Player Fall State");
                }
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }

        private void TryNextAttack()
        {
            if (_elapsedTime >= cooldown)
            {
                _stateController.AerialAttack();
            }
        }
    }
}
