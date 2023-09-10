using ProjectLumina.Character;

namespace ProjectLumina.Player.StateMachine.States
{
    public class FallAttackState : InAirState
    {
        public FallAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                characterFallAttack.FallAttack();
                stateController.InputReader.onAttack += characterFallAttack.FallAttack;
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                stateController.InputReader.onAttack -= characterFallAttack.FallAttack;
                characterFallAttack.FinishFallAttack();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                if (characterFallAttack.IsFallAttacking == false)
                {
                    ChangeToFall();
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                characterFall.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}