using System;
using ProjectLumina.Character;
using ProjectLumina.Player.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [AddComponentMenu("Character/Player/Player State Controller")]
    public class PlayerStateController : CharacterStateController
    {
        public CharacterStateMachine<PlayerStateController> StateMachine { get; private set; }

        [BoxGroup("States"), SerializeField]
        protected CharacterState<PlayerStateController> startingState;

        [BoxGroup("States"), SerializeField]
        protected CharacterState<PlayerStateController>[] states;

        [field: BoxGroup("Player"), SerializeField]
        public InputReader InputReader { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            StateMachine = new(this);
        }

        protected virtual void Start()
        {
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

        public void Jump()
        {
            if (HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.CanJump())
                {
                    ChangeState("Player Jump State");
                }
            }
        }

        public void Roll()
        {
            if (HasCharacterAbility(out CharacterRoll characterRoll))
            {
                ChangeState("Player Roll State");
            }
        }

        public void ChangeState(string stateName)
        {
            CharacterState<PlayerStateController> state = Array.Find(
                states,
                i => i.name == stateName
            );
            StateMachine.ChangeState(state);
        }
    }
}
