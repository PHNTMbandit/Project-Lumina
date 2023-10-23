using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerMeleeAttackState : PlayerGroundedState
    {
        public PlayerMeleeAttackState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                characterMeleeAttack.MeleeAttack();
                stateController.InputReader.onAttack += characterMeleeAttack.MeleeAttack;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                stateController.InputReader.onAttack -= characterMeleeAttack.MeleeAttack;
                characterMeleeAttack.FinishMeleeAttack();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                if (characterMeleeAttack.IsAttacking == false)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}