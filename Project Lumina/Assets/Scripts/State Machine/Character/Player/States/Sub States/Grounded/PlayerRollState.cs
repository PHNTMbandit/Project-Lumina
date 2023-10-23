using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerRollState : PlayerGroundedState
    {
        public PlayerRollState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                characterRoll.RollCharacter();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                characterRoll.FinishRoll();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (stateController.InputReader.AttackInput)
                {
                    if (characterRollAttack.IsRollAttacking == false)
                    {
                        stateController.StateMachine.ChangeState(stateController.RollAttackState);
                    }
                }
            }

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                if (stateController.InputReader.AttackInput == false)
                {
                    if (characterRoll.IsRolling == false)
                    {
                        stateController.StateMachine.ChangeState(stateController.IdleState);
                    }
                }
            }
        }
    }
}