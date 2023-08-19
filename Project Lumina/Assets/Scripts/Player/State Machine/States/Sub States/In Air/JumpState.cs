using ProjectLumina.Abilities;
using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Jump State", menuName = "Project Lumina/States/Jump State")]
    public class JumpState : InAirState
    {
        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.Jump();
            }

            stateController.InputReader.onAttack = TryAttack;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onAttack -= TryAttack;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterFall characterFall))
            {
                if (characterFall.IsFalling())
                {
                    stateController.ChangeState(stateController.GetState("Fall"));
                }
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterJump characterJump))
            {
                characterJump.SetGravityScale();
            }

            if (stateController.HasCharacterAbility(out CharacterMove characterMove))
            {
                characterMove.MoveCharacter(stateController.InputReader.MoveInput.x);
            }
        }
    }
}