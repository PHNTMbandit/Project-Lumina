
using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerFallAttackState : PlayerInAirState
    {
        public PlayerFallAttackState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                characterFallAttack.FallAttack();
                stateController.InputReader.onAttack += characterFallAttack.FallAttack;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                stateController.InputReader.onAttack -= characterFallAttack.FallAttack;
                characterFallAttack.FinishFallAttack();
            }
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

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                if (characterFallAttack.IsFallAttacking == false)
                {
                    stateController.StateMachine.ChangeState(stateController.FallState);
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