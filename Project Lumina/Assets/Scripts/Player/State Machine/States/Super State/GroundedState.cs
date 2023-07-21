namespace ProjectLumina.Player.StateMachine.States
{
    public class GroundedState : State
    {
        public GroundedState(StateController stateController, StateMachine stateMachine, string animBoolName) : base(stateController, stateMachine, animBoolName)
        {
        }

        protected float lastMoveX, moveInput;

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            moveInput = stateController.InputReader.MoveInput;

            if (moveInput != 0)
            {
                lastMoveX = moveInput;
            }

            stateController.SpriteRenderer.flipX = lastMoveX < 0;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            stateController.PlayerMovement.Move(moveInput);
        }
    }
}