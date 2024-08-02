namespace ProjectLumina.StateMachine
{
    public class CharacterStateMachine<T>
        where T : CharacterStateController
    {
        public CharacterState<T> CurrentState { get; private set; }
        private T _stateController;

        public CharacterStateMachine(T stateController)
        {
            _stateController = stateController;
        }

        public void Initialise(CharacterState<T> startingState)
        {
            CurrentState = startingState;
            startingState.OnEnter(_stateController);
        }

        public void ChangeState(CharacterState<T> newState)
        {
            CurrentState.OnExit(_stateController);
            CurrentState = newState;
            newState.OnEnter(_stateController);
        }
    }
}
