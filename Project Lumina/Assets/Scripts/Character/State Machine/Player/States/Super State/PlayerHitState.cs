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
        public float duration = 0.5f;

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

            stateController.CharacterMove.StopCharacter();

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                stateController.ChangeState("Player Idle State");
            }
        }
    }
}
