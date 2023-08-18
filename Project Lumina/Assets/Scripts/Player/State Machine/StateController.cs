using ProjectLumina.Character;
using ProjectLumina.Input;
using System;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Player/State Controller")]
    public class StateController : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        public InputReader InputReader { get; private set; }

        public Animator Animator { get; private set; }
        public CharacterAerialAttack PlayerAerialAttack { get; private set; }
        public CharacterMeleeAttack PlayerMeleeAttack { get; private set; }
        public CharacterFall PlayerFall { get; private set; }
        public CharacterJump PlayerJump { get; private set; }
        public CharacterMove PlayerMove { get; private set; }
        public CharacterWallSlide PlayerWallSlide { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        #endregion Properties

        #region States

        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        #endregion States

        #region Variables

        [SerializeField]
        private State _defaultState;

        [SerializeField]
        private State[] _states;


        #endregion Variables

        #region Unity Callback Functions

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            PlayerAerialAttack = GetComponent<CharacterAerialAttack>();
            PlayerFall = GetComponent<CharacterFall>();
            PlayerJump = GetComponent<CharacterJump>();
            PlayerMeleeAttack = GetComponent<CharacterMeleeAttack>();
            PlayerMove = GetComponent<CharacterMove>();
            PlayerWallSlide = GetComponent<CharacterWallSlide>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            Initialise(_defaultState);
        }

        private void Update()
        {
            CurrentState.LogicUpdate(this);
        }

        private void FixedUpdate()
        {
            CurrentState.PhysicsUpdate(this);
        }

        #endregion Unity Callback Functions

        #region State Functions

        public void Initialise(State startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter(this);
        }

        public void ChangeState(State newState)
        {
            if (newState != null)
            {
                CurrentState.Exit(this);
                PreviousState = CurrentState;
                CurrentState = newState;
                CurrentState.Enter(this);
            }
        }

        public State GetState(string stateName)
        {
            return Array.Find(_states, i => i.name == $"{stateName} State");
        }

        #endregion State Functions
    }
}