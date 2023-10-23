using Pathfinding;
using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/NPC/NPC State Controller")]
    public class NPCStateController : CharacterStateController
    {
        public AIPath AIPath { get; private set; }
        public NPCFallState FallState { get; private set; }
        public NPCIdleState IdleState { get; private set; }
        public NPCMoveState MoveState { get; private set; }
        public NPCShootState ShootState { get; private set; }
        public GameObject Target { get; private set; }
        public float MoveSpeed { get; private set; }

        private CharacterMove _characterMove;

        protected override void Awake()
        {
            base.Awake();

            FallState = new(this, "fall");
            IdleState = new(this, "idle");
            MoveState = new(this, "move");
            ShootState = new(this, "shoot");

            AIPath = GetComponent<AIPath>();
            _characterMove = GetComponent<CharacterMove>();
        }

        private void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            StateMachine.Initialise(IdleState);

            MoveSpeed = _characterMove.MoveSpeed.Value;
            AIPath.maxSpeed = MoveSpeed;
            AIPath.maxAcceleration = _characterMove.MoveSpeed.Value;
        }

        public void UpdateFacingDirection(Vector3 target)
        {
            Vector3 direction = target - transform.position;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}