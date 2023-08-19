using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Fall State", menuName = "Project Lumina/States/Fall State")]
    public class FallState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.InputReader.onAttack = TryAttack;
            stateController.InputReader.onFallAttack = TryFallAttack;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onAttack -= TryAttack;
            stateController.InputReader.onFallAttack = TryFallAttack;
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

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                characterFall.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }

        protected void TryFallAttack()
        {
            if (stateController.HasCharacterAbility(out CharacterFallAttack characterFallAttack))
            {
                if (characterFallAttack.FallAttackCharge)
                {
                    stateController.ChangeState(stateController.GetState("Fall Attack"));
                }
            }
        }
    }
}