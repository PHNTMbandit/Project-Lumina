using ProjectLumina.Character;
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

        public override void OnUpdate(NPCStateController stateController)
        {
            base.OnUpdate(stateController);

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                if (characterShoot.IsShooting == false)
                {
                    stateController.ChangeState("NPC Idle State");
                }
            }
        }
    }
}
