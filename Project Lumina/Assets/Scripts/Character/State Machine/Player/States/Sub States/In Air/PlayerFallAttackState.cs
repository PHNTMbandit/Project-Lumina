using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [CreateAssetMenu(
        fileName = "Player Fall Attack State",
        menuName = "Character States/Player/Fall Attack State",
        order = 0
    )]
    public class PlayerFallAttackState : PlayerInAirState
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

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                stateController.ChangeState("Player Fall State");
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterFall.SetGravityScale();
            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }
    }
}
