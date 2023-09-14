using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class HitState : State
    {
        public HitState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }
    }
}