using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerDashState : PlayerInAirState
    {
        public PlayerDashState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                characterDash.UseDash();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.HasCharacterAbility(out CharacterDash characterDash))
            {
                if (characterDash.IsDashing == false)
                {
                    stateController.StateMachine.ChangeState(stateController.FallState);
                }
            }
        }
    }
}