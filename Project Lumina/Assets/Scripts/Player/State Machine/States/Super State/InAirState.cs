using ProjectLumina.Character;

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
                    stateController.ChangeState("Wall Slide");
                }
            }
        }

        protected void TryAttack()
        {
            if (stateController.InputReader.MoveInput.y < -0.9f)
            {
                if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
                {
                    if (characterFallAttack.IsFallAttacking == false)
                    {
                        stateController.ChangeState("Fall Attack");
                    }
                }
            }
            else
            {
                if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
                {
                    if (characterAerialAttack.IsAttacking == false)
                    {
                        stateController.ChangeState("Aerial Attack");
                    }
                }
            }
        }

        protected void TryDash()
        {
            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.CurrentDashCharges > 0)
                {
                    stateController.ChangeState("Dash");
                }
            }
        }

        protected void ChangeToFall()
        {
            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                stateController.ChangeState("Fall");
            }
        }
    }
}