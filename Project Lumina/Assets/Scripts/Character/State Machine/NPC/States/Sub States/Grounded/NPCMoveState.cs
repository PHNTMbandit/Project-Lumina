using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [CreateAssetMenu(
        fileName = "NPC Move State",
        menuName = "Character States/NPC/Move State",
        order = 0
    )]
    public class NPCMoveState : NPCGroundedState
    {
        public override void OnUpdate(NPCStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.AIPath.velocity.x == 0 && stateController.AIPath.hasPath == false)
            {
                stateController.ChangeState("NPC Idle State");
            }

            stateController.UpdateFacingDirection(stateController.Target.transform.position);
        }
    }
}
