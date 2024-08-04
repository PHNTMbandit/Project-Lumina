using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Dash State",
        menuName = "Character States/Player/Dash State",
        order = 0
    )]
    public class PlayerDashState : PlayerInAirState
    {
        public float cooldown,
            duration = 0.5f;

        private float _enterTime,
            _elapsedTime;

        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            _enterTime = Time.time;
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            base.OnUpdate(stateController);

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                if (stateController.HasCharacterAbility(out CharacterDash dash))
                {
                    stateController.ChangeState("Player Fall State");
                }
            }
        }
    }
}
