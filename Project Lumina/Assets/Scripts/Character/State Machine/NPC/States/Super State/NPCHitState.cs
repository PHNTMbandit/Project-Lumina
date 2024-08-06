using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.StateMachine.Character.NPC
{
    [CreateAssetMenu(
        fileName = "NPC Hit State",
        menuName = "Character States/NPC/Hit State",
        order = 0
    )]
    public class NPCHitState : NPCState
    {
        public override void OnEnter(NPCStateController stateController)
        {
            base.OnEnter(stateController);

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                characterShoot.FinishShoot();
            }

            stateController.AIPath.maxSpeed = 0;
        }

        public override void OnExit(NPCStateController stateController)
        {
            base.OnExit(stateController);

            stateController.AIPath.maxSpeed = stateController.MoveSpeed;
        }
    }
}
