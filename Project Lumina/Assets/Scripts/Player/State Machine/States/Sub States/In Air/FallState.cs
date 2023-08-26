using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class FallState : InAirState
    {
        public FallState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
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

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                characterFall.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}