using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCMoveState : NPCGroundedState
    {
        public NPCMoveState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.AIPath.velocity.x == 0 && stateController.AIPath.hasPath == false)
            {
                stateController.StateMachine.ChangeState(stateController.IdleState);
            }

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                if (characterShoot.IsShooting)
                {
                    stateController.StateMachine.ChangeState(stateController.ShootState);
                }
            }

            stateController.UpdateFacingDirection(stateController.Target.transform.position);
        }
    }
}