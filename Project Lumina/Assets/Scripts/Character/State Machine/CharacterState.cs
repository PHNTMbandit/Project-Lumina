using UnityEngine;

namespace ProjectLumina.StateMachine
{
    public abstract class CharacterState<T> : ScriptableObject
        where T : CharacterStateController
    {
        public string stateAnimationName;

        public abstract void OnEnter(T stateController);

        public abstract void OnExit(T stateController);

        public abstract void OnUpdate(T stateController);

        public abstract void OnFixedUpdate(T stateController);
    }
}
