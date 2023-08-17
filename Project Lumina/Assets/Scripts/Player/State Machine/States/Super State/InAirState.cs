namespace ProjectLumina.Player.StateMachine.States
{
    public class InAirState : State
    {
        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerFall.IsFalling)
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(stateController.InputReader.MoveInput);
        }
    }
}