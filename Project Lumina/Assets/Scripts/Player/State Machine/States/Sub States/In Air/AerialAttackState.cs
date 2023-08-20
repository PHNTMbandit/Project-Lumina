using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Aerial Melee Attack State", menuName = "Project Lumina/States/Aerial Attack State")]
    public class AerialAttackState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.UseAerialAttack();
                characterAerialAttack.onComboFinished = ChangeToFall;
                stateController.InputReader.onAttack = characterAerialAttack.UseAerialAttack;
            }
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.onComboFinished -= ChangeToFall;
                stateController.InputReader.onAttack -= characterAerialAttack.UseAerialAttack;
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

            if (stateController.HasCharacterAbility(out CharacterAerialAttack characterAerialAttack))
            {
                characterAerialAttack.SetGravityScale();
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