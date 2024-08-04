using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Roll Attack State",
        menuName = "Character States/Player/Roll Attack State",
        order = 0
    )]
    public class PlayerRollAttackState : PlayerGroundedState
    {
        public float duration = 0.5f;

        private float _enterTime,
            _elapsedTime;

        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            _enterTime = Time.time;

            if (stateController.HasCharacterAbility(out CharacterRollAttack rollAttack))
            {
                rollAttack.RollAttack();
            }
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                stateController.ChangeState("Player Idle State");
            }
        }
    }
}
