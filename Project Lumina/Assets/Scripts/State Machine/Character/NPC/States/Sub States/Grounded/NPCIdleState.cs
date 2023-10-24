using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCIdleState : NPCGroundedState
    {
        public NPCIdleState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.AIPath.velocity.x != 0 && stateController.AIPath.hasPath)
            {
                stateController.StateMachine.ChangeState(stateController.MoveState);
            }

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                if (characterShoot.IsShooting)
                {
                    stateController.StateMachine.ChangeState(stateController.ShootState);
                }
            }
        }
    }
}