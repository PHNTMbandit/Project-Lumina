using UnityEngine;

namespace ProjectLumina.StateMachine
{
    public abstract class StateController : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }

        protected virtual void Awake()
        {
            StateMachine = new();
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState.OnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.CurrentState.OnFixedUpdate();
        }
    }
}