using ProjectLumina.Abilities;
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

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterRollAttack characterRollAttack))
            {
                if (stateController.InputReader.AttackInput)
                {
                    stateController.ChangeState(stateController.GetState("Roll Attack"));

                }
            }
            else if (stateController.HasCharacterAbility(out CharacterRoll characterRoll))
            {
                if (characterRoll.IsRolling == false)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }
    }
}