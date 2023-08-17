namespace ProjectLumina.Player.StateMachine.States
{
    public class InAirState : State
    {
        protected StateController stateController;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            this.stateController = stateController;

            stateController.InputReader.onAttack = TryAttack;
            stateController.PlayerMeleeAttack.ResetCombo();
        }
        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onAttack -= TryAttack;
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(stateController.InputReader.MoveInput);
        }

        protected void TryAttack()
        {
            if (stateController.PlayerAerialAttack.AerialAttackCharge)
            {
                stateController.ChangeState(stateController.GetState("Aerial Attack"));
            }
        }
    }
}