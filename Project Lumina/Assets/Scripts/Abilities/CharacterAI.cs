using Pathfinding;
using ProjectLumina.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.AI
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/Character AI")]
    public class CharacterAI : CharacterAbility
    {
        public GameObject Target { get; private set; }
        public float MoveSpeed { get; private set; }

        [BoxGroup("AI States"), SerializeField]
        private string[] _AIStates;

        [BoxGroup("AI States"), SerializeField]
        private string _defaultAIState;

        private string _currentState;
        private AIPath _AIPath;
        private Animator _animator;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _AIPath = GetComponent<AIPath>();
            _animator = GetComponent<Animator>();
            _characterMove = GetComponent<CharacterMove>();

            _AIPath.onSearchPath += SetFacingDirection;
        }

        private void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");

            MoveSpeed = _characterMove.MoveSpeed.Value;
            _AIPath.maxSpeed = MoveSpeed;
            _AIPath.maxAcceleration = _characterMove.MoveSpeed.Value;

            InitialiseState();
        }

        private void InitialiseState()
        {
            _currentState = _defaultAIState;
            _animator.SetBool(_currentState, true);
        }

        public void ChangeState(string AIState)
        {
            _animator.SetBool(_currentState, false);
            _currentState = AIState;
            _animator.SetBool(_currentState, true);
        }

        private void SetFacingDirection()
        {
            Vector3 direction = _AIPath.steeringTarget - transform.position;

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