namespace ProjectLumina.Player.StateMachine.States
{
    public class IdleState : GroundedState
    {
        public IdleState(StateController stateController, StateMachine stateMachine, string animBoolName) : base(stateController, stateMachine, animBoolName)
        {
        }

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

            if (moveInput != 0)
            {
                stateMachine.ChangeState(stateController.MoveState);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}