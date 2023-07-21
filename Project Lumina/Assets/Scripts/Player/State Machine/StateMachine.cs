using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public void Initialise(State startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            PreviousState = CurrentState;
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}