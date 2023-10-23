using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
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

            if (stateController.HasCharacterAbility(out CharacterSprint characterSprint))
            {
                if (stateController.InputReader.SprintInput)
                {
                    stateController.StateMachine.ChangeState(stateController.SprintState);
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}