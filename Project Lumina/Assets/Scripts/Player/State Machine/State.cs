namespace ProjectLumina.Player.StateMachine
{
    public abstract class State
    {
        public string stateName;
        protected string animationStateName;
        protected StateController stateController;

        public State(string stateName, string animationStateName, StateController stateController)
        {
            this.stateName = stateName;
            this.animationStateName = animationStateName;
            this.stateController = stateController;
        }

        public virtual void Enter()
        {
            stateController.Animator.SetBool(animationStateName, true);
            stateController.Damageable.onDamaged.AddListener(ChangeToHit);

        }

        public virtual void Exit()
        {
            stateController.Animator.SetBool(animationStateName, false);
            stateController.Damageable.onDamaged.RemoveListener(ChangeToHit);
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        protected void ChangeToHit()
        {
            stateController.ChangeState("Hit");
        }
    }
}