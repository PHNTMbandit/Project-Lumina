using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCHitState : NPCState
    {
        public NPCHitState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterShoot characterShoot))
            {
                characterShoot.FinishShoot();
            }

            stateController.AIPath.maxSpeed = 0;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.AIPath.maxSpeed = stateController.MoveSpeed;
        }
    }
}