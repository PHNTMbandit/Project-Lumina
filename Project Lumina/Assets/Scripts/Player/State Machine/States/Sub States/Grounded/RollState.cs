using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class RollState : GroundedState
    {
        public RollState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                characterRoll.RollCharacter();
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                characterRoll.FinishRoll();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (stateController.InputReader.AttackInput)
                {
                    if (characterRollAttack.IsRollAttacking == false)
                    {
                        stateController.ChangeState("Roll Attack");
                    }
                }
            }

            if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                if (stateController.InputReader.AttackInput == false)
                {
                    if (characterRoll.IsRolling == false)
                    {
                        stateController.ChangeState("Idle");
                    }
                }
            }
        }
    }
}