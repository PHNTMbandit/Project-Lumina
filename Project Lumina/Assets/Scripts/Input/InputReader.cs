using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectLumina.Input
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Project Lumina/Input Reader")]
    public class InputReader : ScriptableObject, GameControls.IPlayerActions
    {
        #region Variables

        public GameControls GameControls { get; private set; }
        public float MoveInput { get; private set; }

        #endregion Variables

        #region Unity Callback Functions

        private void OnEnable()
        {
            if (GameControls == null)
            {
                GameControls = new GameControls();
                GameControls.Player.AddCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        #endregion Unity Callback Functions

        #region Player Actions

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<float>();
        }

        #endregion Player Actions

        #region Device Map Actions

        public void DisableGameplayInput()
        {
            GameControls.Player.Disable();
        }

        public void EnableGameplayInput()
        {
            GameControls.Player.Enable();
        }

        public void DisableAllInput()
        {
            GameControls.Player.Disable();
        }

        public void EnableAllInput()
        {
            GameControls.Player.Disable();
        }

        #endregion Device Map Actions
    }
}