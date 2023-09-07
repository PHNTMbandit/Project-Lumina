using ProjectLumina.Character;

namespace ProjectLumina.Player.StateMachine.States
{
    public class JumpState : InAirState
    {
        public JumpState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.Jump();
            }

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

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                if (characterFall.IsFalling())
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}