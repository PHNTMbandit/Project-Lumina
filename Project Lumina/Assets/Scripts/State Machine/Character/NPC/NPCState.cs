using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCState : State
    {
        protected NPCStateController stateController;
        protected string stateAnimationName;

        protected NPCState(NPCStateController stateController, string stateAnimationName)
        {
            this.stateController = stateController;
            this.stateAnimationName = stateAnimationName;
        }

        public override void OnEnter()
        {
            stateController.Animator.SetBool(stateAnimationName, true);
        }

        public override void OnExit()
        {
            stateController.Animator.SetBool(stateAnimationName, false);
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }
    }
}