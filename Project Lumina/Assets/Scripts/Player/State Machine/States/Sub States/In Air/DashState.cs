using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class DashState : InAirState
    {
        public DashState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.UseDash();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.IsDashing == false)
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }
        }
    }
}