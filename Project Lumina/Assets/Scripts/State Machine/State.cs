namespace ProjectLumina.StateMachine
{
    public abstract class State
    {
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}