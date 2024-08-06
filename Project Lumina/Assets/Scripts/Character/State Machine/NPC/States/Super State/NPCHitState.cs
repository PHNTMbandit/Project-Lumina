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
        public float duration = 0.5f;

        private float _enterTime,
            _elapsedTime;

        public override void OnEnter(NPCStateController stateController)
        {
            base.OnEnter(stateController);

            _enterTime = Time.time;
            stateController.AIPath.maxSpeed = 0;
        }

        public override void OnExit(NPCStateController stateController)
        {
            base.OnExit(stateController);

            stateController.AIPath.maxSpeed = stateController.MoveSpeed;
        }

        public override void OnUpdate(NPCStateController stateController)
        {
            base.OnUpdate(stateController);

            _elapsedTime = Time.time - _enterTime;

            if (_elapsedTime >= duration)
            {
                stateController.ChangeState("NPC Idle State");
            }
        }
    }
}
