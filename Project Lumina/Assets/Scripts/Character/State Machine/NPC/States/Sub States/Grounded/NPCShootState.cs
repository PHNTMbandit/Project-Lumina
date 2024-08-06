using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [CreateAssetMenu(
        fileName = "NPC Shoot State",
        menuName = "Character States/NPC/Shoot State",
        order = 0
    )]
    public class NPCShootState : NPCGroundedState
    {
        public override void OnEnter(NPCStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.UpdateFacingDirection(stateController.Target.transform.position);
        }
    }
}
