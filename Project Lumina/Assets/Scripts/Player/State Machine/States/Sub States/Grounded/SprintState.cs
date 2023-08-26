using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class SprintState : GroundedState
    {
        public SprintState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onAttack = TryAttack;
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onAttack -= TryAttack;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (moveInput == 0)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }

            if (stateController.InputReader.SprintInput == false)
            {
                stateController.ChangeState(stateController.GetState("Move"));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterSprint characterSprint))
            {
                characterSprint.Sprint(moveInput);
            }
        }
    }
}