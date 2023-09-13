using ProjectLumina.Character;

namespace ProjectLumina.Player.StateMachine.States
{
    public class AerialAttackState : InAirState
    {
        public AerialAttackState(string stateName, string animationStateName, StateController stateController) : base(stateName, animationStateName, stateController)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.AerialAttack();
                stateController.InputReader.onAttack += characterAerialAttack.AerialAttack;
            }
        }

        public override void Exit()
        {
            base.Exit();

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                stateController.InputReader.onAttack -= characterAerialAttack.AerialAttack;
                characterAerialAttack.FinishAerialAttack();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState("Idle");
                }
            }

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                if (characterAerialAttack.IsAttacking == false)
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
                if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
                {
                    if (characterAerialAttack.IsSlowStop == false)
                    {
                        characterFall.SetGravityScale();
                    }
                }
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}