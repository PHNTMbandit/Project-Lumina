using ProjectLumina.Character;
using ProjectLumina.Player.Input;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.Player
{
    [AddComponentMenu("Character/Player/Player State Controller")]
    public class PlayerStateController : CharacterStateController
    {
        [field: SerializeField]
        public InputReader InputReader { get; private set; }

        public PlayerAerialAttackState AerialAttackState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerDeadState DeadState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        public PlayerFallAttackState FallAttackState { get; private set; }
        public PlayerHitState HitState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMeleeAttackState MeleeAttackState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerRollState RollState { get; private set; }
        public PlayerRollAttackState RollAttackState { get; private set; }
        public PlayerSprintState SprintState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            abilities = GetComponents<CharacterAbility>();

            AerialAttackState = new(this, "aerial attack");
            DashState = new(this, "dash");
            DeadState = new(this, "dead");
            FallState = new(this, "fall");
            FallAttackState = new(this, "fall attack");
            HitState = new(this, "hit");
            IdleState = new(this, "idle");
            MeleeAttackState = new(this, "melee attack");
            JumpState = new(this, "jump");
            MoveState = new(this, "move");
            RollState = new(this, "roll");
            RollAttackState = new(this, "roll attack");
            SprintState = new(this, "sprint");
            WallSlideState = new(this, "wall slide");
        }

        protected virtual void Start()
        {
            StateMachine.Initialise(IdleState);
        }

        #region Ability Functions

        public void TryAerialAttack()
        {
            if (InputReader.MoveInput.y < -0.9f)
            {
                if (HasCharacterAbility(out CharacterFallAttack characterFallAttack))
                {
                    if (characterFallAttack.IsFallAttacking == false)
                    {
                        StateMachine.ChangeState(FallAttackState);
                    }
                }
            }
            else
            {
                if (HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
                {
                    if (characterAerialAttack.IsAttacking == false)
                    {
                        StateMachine.ChangeState(AerialAttackState);
                    }
                }
            }
        }

        public void TryAttack()
        {
            if (HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                if (characterMeleeAttack.IsAttacking == false)
                {
                    StateMachine.ChangeState(MeleeAttackState);
                }
            }
        }

        public void TryDash()
        {
            if (HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.CurrentDashCharges > 0)
                {
                    StateMachine.ChangeState(DashState);
                }
            }
        }

        public void TryJump()
        {
            if (HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.CanJump())
                {
                    StateMachine.ChangeState(JumpState);
                }
            }
        }

        public void TryRoll()
        {
            if (HasCharacterAbility(out CharacterRoll characterRoll))
            {
                StateMachine.ChangeState(RollState);
            }
        }

        public void TryWallJump()
        {
            if (HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                characterWallSlide.Jump();

                StateMachine.ChangeState(JumpState);
            }
        }

        public void TryWallSlide()
        {
            if (HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                if (characterWallSlide.CanWallSlide && InputReader.MoveInput.x != 0)
                {
                    StateMachine.ChangeState(WallSlideState);
                }
            }
        }

        #endregion
    }
}