using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerRollAttackState : PlayerGroundedState
    {
        public PlayerRollAttackState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.RollAttack();
                stateController.InputReader.onAttack += characterRollAttack.RollAttack;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                stateController.InputReader.onAttack -= characterRollAttack.RollAttack;
                characterRollAttack.FinishRollAttack();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (characterRollAttack.IsRollAttacking == false)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }
    }
}