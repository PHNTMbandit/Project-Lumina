using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [CreateAssetMenu(
        fileName = "NPC Idle State",
        menuName = "Character States/NPC/Idle State",
        order = 0
    )]
    public class NPCIdleState : NPCGroundedState
    {
        public override void OnUpdate(NPCStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.AIPath.velocity.x != 0 && stateController.AIPath.hasPath)
            {
                stateController.ChangeState("NPC Move State");
            }

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                if (characterShoot.IsShooting)
                {
                    stateController.ChangeState("NPC Shoot State");
                }
            }
        }
    }
}
