using ProjectLumina.Input;
using ProjectLumina.Player.StateMachine.States;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public abstract class StateController : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        public InputReader InputReader { get; private set; }

        public Animator Animator { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public StateMachine StateMachine { get; private set; }

        #endregion Properties

        #region States

        public IdleState IdleState { get; private set; }
        public MoveState MoveState { get; private set; }

        #endregion States

        private void Awake()
        {
            StateMachine = new StateMachine();
            IdleState = new(this, StateMachine, "idle");
            MoveState = new(this, StateMachine, "moving");

            Animator = GetComponent<Animator>();
            PlayerMovement = GetComponent<PlayerMovement>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            StateMachine.Initialise(IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.FixedUpdate();
        }
    }
}