using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    public abstract class State : ScriptableObject
    {
        [SerializeField]
        private string _animationStateName;

        public virtual void Enter(StateController stateController)
        {
            stateController.Animator.SetBool(_animationStateName, true);
        }

        public virtual void Exit(StateController stateController)
        {
            stateController.Animator.SetBool(_animationStateName, false);
        }

        public virtual void LogicUpdate(StateController stateController)
        {
        }

        public virtual void PhysicsUpdate(StateController stateController)
        {
        }
    }
}