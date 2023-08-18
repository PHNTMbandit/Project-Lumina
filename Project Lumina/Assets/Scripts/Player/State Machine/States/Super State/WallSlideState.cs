using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Wall Slide State", menuName = "Project Lumina/States/Wall Slide State")]
    public class WallSlideState : State
    {
        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

        }
    }
}