using ProjectLumina.Character;

namespace ProjectLumina.Player.StateMachine.States
{
    public class RollAttackState : GroundedState
    {
        public RollAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                characterRollAttack.RollAttack();
                stateController.InputReader.onAttack += characterRollAttack.RollAttack;
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                stateController.InputReader.onAttack -= characterRollAttack.RollAttack;
                characterRollAttack.FinishRollAttack();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (characterRollAttack.IsRollAttacking == false)
                {
                    ChangeToIdle();
                }
            }
        }
    }
}