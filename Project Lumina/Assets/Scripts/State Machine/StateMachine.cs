using UnityEngine;

namespace ProjectLumina.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialise(State startingState)
        {
            CurrentState = startingState;
            startingState.OnEnter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.OnExit();
            CurrentState = newState;
            newState.OnEnter();
        }
    }
}