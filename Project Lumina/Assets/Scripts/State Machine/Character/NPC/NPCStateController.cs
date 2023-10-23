using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [AddComponentMenu("Character/NPC/NPC State Controller")]
    public class NPCStateController : CharacterStateController
    {
        public NPCFallState FallState { get; private set; }
        public NPCIdleState IdleState { get; private set; }
        public NPCMoveState MoveState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            FallState = new(this, "fall");
            IdleState = new(this, "idle");
            MoveState = new(this, "move");
        }

        protected virtual void Start()
        {
            StateMachine.Initialise(IdleState);
        }
    }
}