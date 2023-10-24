using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerSprintState : PlayerGroundedState
    {
        public PlayerSprintState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onAttack = stateController.TryAttack;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onAttack -= stateController.TryAttack;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (moveInput.x == 0)
            {
                stateController.StateMachine.ChangeState(stateController.IdleState);
            }

            if (stateController.InputReader.SprintInput == false)
            {
                stateController.StateMachine.ChangeState(stateController.MoveState);
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (stateController.HasCharacterAbility(out CharacterSprint characterSprint))
            {
                characterSprint.Sprint(moveInput.x);
            }
        }
    }
}