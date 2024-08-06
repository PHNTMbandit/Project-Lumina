using System;
using System.Linq;
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

        private PlayerAerialAttackState[] _aerialAttackStates;
        private PlayerMeleeAttackState[] _meleeAttackStates;

        protected override void Awake()
        {
            base.Awake();

            StateMachine = new(this);
            _aerialAttackStates = states.OfType<PlayerAerialAttackState>().ToArray();
            _meleeAttackStates = states.OfType<PlayerMeleeAttackState>().ToArray();
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

        public void AerialAttack()
        {
            if (InputReader.MoveInput.y >= 0)
            {
                if (HasCharacterAbility(out CharacterAerialAttack aerialAttack))
                {
                    if (aerialAttack.CanNextCombo(_aerialAttackStates.Length))
                    {
                        ChangeState($"Player Aerial Attack {aerialAttack.CurrentComboIndex} State");
                    }
                }
            }
            else if (InputReader.MoveInput.y < 0)
            {
                FallAttack();
            }
        }

        public void FallAttack()
        {
            if (HasCharacterAbility(out CharacterFallAttack fallAttack))
            {
                ChangeState("Player Fall Attack State");
            }
        }

        public void Dash()
        {
            if (HasCharacterAbility(out CharacterDash dash))
            {
                if (dash.CurrentDashCharges > 0)
                {
                    ChangeState($"Player Dash State");
                    dash.Dash();
                }
            }
        }

        public void MeleeAttack()
        {
            if (HasCharacterAbility(out CharacterMeleeAttack meleeAttack))
            {
                if (meleeAttack.CanNextCombo(_meleeAttackStates.Length))
                {
                    ChangeState($"Player Melee Attack {meleeAttack.CurrentComboIndex} State");
                }
            }
        }

        public void Jump()
        {
            if (HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.CanJump())
                {
                    ChangeState("Player Jump State");
                    characterJump.Jump();
                }
            }
        }

        public void Roll()
        {
            if (HasCharacterAbility(out CharacterRoll characterRoll))
            {
                ChangeState("Player Roll State");
                characterRoll.RollCharacter();
            }
        }

        public void RollAttack()
        {
            if (HasCharacterAbility(out CharacterRollAttack rollAttack))
            {
                ChangeState("Player Roll Attack State");
                rollAttack.RollAttack();
            }
        }

        public void WallJump()
        {
            if (HasCharacterAbility(out CharacterWallJump wallJump))
            {
                if (wallJump.CanWallJump())
                {
                    ChangeState("Player Jump State");
                    wallJump.WallJump();
                }
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
