using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    public class InAirState : State
    {
        public InAirState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateController.InputReader.onDash = TryDash;

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                characterMeleeAttack.ResetMeleeAttackCombo();
            }
        }

        public override void Exit()
        {
            base.Exit();

            stateController.InputReader.onDash -= TryDash;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterWallSlide characterWallSlide))
            {
                if (characterWallSlide.CanWallSlide && stateController.InputReader.MoveInput.x != 0)
                {
                    stateController.ChangeState(stateController.GetState("Wall Slide"));
                }
            }
        }

        protected void TryAttack()
        {
            if (stateController.InputReader.MoveInput.y < 0)
            {
                if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
                {
                    stateController.ChangeState(stateController.GetState("Fall Attack"));
                }
            }
            else
            {
                if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
                {
                    stateController.ChangeState(stateController.GetState("Aerial Attack"));
                }
            }
        }

        protected void TryDash()
        {
            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.CurrentDashCharges > 0)
                {
                    stateController.ChangeState(stateController.GetState("Dash"));
                }
            }
        }

        protected void ChangeToFall()
        {
            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
        }
    }
}