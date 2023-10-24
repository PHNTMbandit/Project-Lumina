using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerJumpState : PlayerInAirState
    {
        public PlayerJumpState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.Jump();
            }

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

            if (stateController.CharacterFall.IsFalling())
            {
                stateController.StateMachine.ChangeState(stateController.FallState);
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.SetGravityScale();
            }
        }
    }
}