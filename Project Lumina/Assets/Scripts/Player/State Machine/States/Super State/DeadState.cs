using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class DeadState : State
    {
        public DeadState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }
    }
}