namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerHitState : PlayerState
    {
        public PlayerHitState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            stateController.CharacterMove.StopCharacter();
        }
    }
}