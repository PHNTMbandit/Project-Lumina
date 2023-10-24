using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerFallState : PlayerInAirState
    {
        public PlayerFallState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onAttack = stateController.TryAerialAttack;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onAttack -= stateController.TryAerialAttack;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterFall.SetGravityScale();
            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
        }
    }
}