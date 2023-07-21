using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    public class State
    {
        protected StateController stateController;
        protected StateMachine stateMachine;
        protected float startTime;
        private string _animBoolName;

        public State(StateController stateController, StateMachine stateMachine, string animBoolName)
        {
            this.stateController = stateController;
            this.stateMachine = stateMachine;
            this._animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            stateController.Animator.SetBool(_animBoolName, true);
            startTime = Time.time;
        }

        public virtual void Exit()
        {
            stateController.Animator.SetBool(_animBoolName, false);
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }
    }
}