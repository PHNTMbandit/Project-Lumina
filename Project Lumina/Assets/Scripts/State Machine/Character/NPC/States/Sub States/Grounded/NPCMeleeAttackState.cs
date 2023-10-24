using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCMeleeAttackState : NPCGroundedState
    {
        public NPCMeleeAttackState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterMeleeAttack characterMeleeAttack))
            {
                if (characterMeleeAttack.IsAttacking == false)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }
    }
}