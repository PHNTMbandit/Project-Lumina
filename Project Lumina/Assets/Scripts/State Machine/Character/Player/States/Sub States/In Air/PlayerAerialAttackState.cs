using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerAerialAttackState : PlayerInAirState
    {
        public PlayerAerialAttackState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.AerialAttack();
                stateController.InputReader.onAttack += characterAerialAttack.AerialAttack;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                stateController.InputReader.onAttack -= characterAerialAttack.AerialAttack;
                characterAerialAttack.FinishAerialAttack();
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

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                if (characterAerialAttack.IsAttacking == false)
                {
                    stateController.StateMachine.ChangeState(stateController.FallState);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterMove.MoveCharacter(stateController.InputReader.MoveInput.x);

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                if (characterAerialAttack.IsSlowStop == false)
                {
                    stateController.CharacterFall.SetGravityScale();
                }
            }
        }
    }
}