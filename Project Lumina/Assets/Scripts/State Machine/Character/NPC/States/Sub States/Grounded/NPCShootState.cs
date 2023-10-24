using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCShootState : NPCGroundedState
    {
        public NPCShootState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.UpdateFacingDirection(stateController.Target.transform.position);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                if (characterShoot.IsShooting == false)
                {
                    stateController.StateMachine.ChangeState(stateController.IdleState);
                }
            }
        }
    }
}