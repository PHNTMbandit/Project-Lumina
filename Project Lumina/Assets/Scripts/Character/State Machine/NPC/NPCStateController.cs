using System;
using Pathfinding;
using ProjectLumina.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/NPC/NPC State Controller")]
    public class NPCStateController : CharacterStateController
    {
        public AIPath AIPath { get; private set; }
        public GameObject Target { get; private set; }
        public float MoveSpeed { get; private set; }
        public CharacterStateMachine<NPCStateController> StateMachine { get; private set; }

        [BoxGroup("States"), SerializeField]
        protected CharacterState<NPCStateController> startingState;

        [BoxGroup("States"), SerializeField]
        protected CharacterState<NPCStateController>[] states;

        private CharacterMove _characterMove;

        protected override void Awake()
        {
            base.Awake();

            StateMachine = new(this);
            AIPath = GetComponent<AIPath>();
            _characterMove = GetComponent<CharacterMove>();
        }

        protected virtual void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            MoveSpeed = _characterMove.MoveSpeed.Value;
            AIPath.maxSpeed = MoveSpeed;
            AIPath.maxAcceleration = _characterMove.MoveSpeed.Value;

            StateMachine.Initialise(startingState);
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState.OnUpdate(this);
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.CurrentState.OnFixedUpdate(this);
        }

        public void ChangeState(string stateName)
        {
            CharacterState<NPCStateController> state = Array.Find(states, i => i.name == stateName);
            StateMachine.ChangeState(state);
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
