using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Fall Attack State", menuName = "Project Lumina/States/Fall Attack State")]
    public class FallAttackState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                characterFallAttack.UseFallAttack();
                characterFallAttack.onFallAttackFinished = ChangeToFall;
            }
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);


            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                characterFallAttack.onFallAttackFinished -= ChangeToFall;
            }

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.ResetAerialCombo();
            }
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                if (characterJump.IsGrounded)
                {
                    stateController.ChangeState(stateController.GetState("Idle"));
                }
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                characterFallAttack.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }

        private void ChangeToFall()
        {
            stateController.ChangeState(stateController.GetState("Fall"));
        }
    }
}