using ProjectLumina.Character;

namespace ProjectLumina.Player.StateMachine.States
{
    public class MoveState : GroundedState
    {
        public MoveState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
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

            if (stateController.InputReader.SprintInput)
            {
                stateController.ChangeState(stateController.GetState("Sprint"));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(moveInput);
            }
        }
    }
}