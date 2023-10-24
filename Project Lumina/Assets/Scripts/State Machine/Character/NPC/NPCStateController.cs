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
        #region Properties

        public AIPath AIPath { get; private set; }
        public NPCDeadState DeadState { get; private set; }
        public NPCFallState FallState { get; private set; }
        public NPCHitState HitState { get; private set; }
        public NPCIdleState IdleState { get; private set; }
        public NPCMoveState MoveState { get; private set; }
        public NPCShootState ShootState { get; private set; }
        public GameObject Target { get; private set; }
        public float MoveSpeed { get; private set; }

        #endregion

        #region Variables

        private CharacterMove _characterMove;

        #endregion

        #region Unity Callback Functions

        protected override void Awake()
        {
            base.Awake();

            DeadState = new(this, "dead");
            FallState = new(this, "fall");
            HitState = new(this, "hit");
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

        #endregion

        #region Other Functions

        public void ChangeToHitState()
        {
            StateMachine.ChangeState(HitState);
        }

        public void ChangeToIdleState()
        {
            StateMachine.ChangeState(IdleState);
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

        #endregion
    }
}