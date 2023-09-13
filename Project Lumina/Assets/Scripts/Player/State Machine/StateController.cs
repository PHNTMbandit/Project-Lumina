using ProjectLumina.Capabilities;
using ProjectLumina.Character;
using ProjectLumina.Player.Input;
using ProjectLumina.Player.StateMachine.States;
using System;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Damageable))]
    [AddComponentMenu("Player/State Controller")]
    public class StateController : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        public InputReader InputReader { get; private set; }

        public Animator Animator { get; private set; }
        public CharacterAbility[] CharacterAbilities { get; private set; }
        public Damageable Damageable { get; private set; }

        #endregion Properties

        #region States

        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        #endregion States

        #region Variables
        private State[] _states;

        #endregion Variables

        #region Unity Callback Functions

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            CharacterAbilities = GetComponents<CharacterAbility>();
            Damageable = GetComponent<Damageable>();

            _states = new State[]
            {
                new AerialAttackState("Aerial Attack", "aerial attack", this),
                new DashState("Dash", "dash", this),
                new FallState("Fall", "fall", this),
                new FallAttackState("Fall Attack", "fall attack", this),
                new HitState("Hit", "hit", this),
                new IdleState("Idle", "idle", this),
                new JumpState("Jump", "jump", this),
                new MeleeAttackState("Melee Attack", "melee attack", this),
                new MoveState("Move", "move", this),
                new RollAttackState("Roll Attack", "roll attack", this),
                new RollState("Roll", "roll", this),
                new SprintState("Sprint", "sprint", this),
                new WallSlideState("Wall Slide", "wall slide", this)
            };
        }

        private void Start()
        {
            Initialise(GetState("Idle"));
        }

        private void Update()
        {
            CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            CurrentState.PhysicsUpdate();
        }

        #endregion Unity Callback Functions

        #region State Functions

        public void Initialise(State startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(string stateName)
        {
            var newState = GetState(stateName);

            if (newState != null)
            {
                CurrentState.Exit();
                PreviousState = CurrentState;
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public State GetState(string stateName)
        {
            return Array.Find(_states, i => i.stateName == stateName);
        }

        public bool HasCharacterAbility<T>(out T characterAbility) where T : CharacterAbility
        {
            T ability = (T)Array.Find(CharacterAbilities, i => i.GetType() == typeof(T));
            characterAbility = ability;

            if (ability != null)
            {
                if (ability.IsUnlocked)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion State Functions
    }
}