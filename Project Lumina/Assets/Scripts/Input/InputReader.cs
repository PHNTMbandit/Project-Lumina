using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectLumina.Input
{
    [CreateAssetMenu(fileName = "Input Reader", menuName = "Project Lumina/Input Reader")]
    public class InputReader : ScriptableObject, GameControls.IPlayerActions
    {
        #region Variables

        public GameControls GameControls { get; private set; }
        public bool AttackInput { get; private set; }
        public bool JumpInputPress { get; private set; }
        public bool JumpInputRelease { get; private set; }
        public Vector2 MoveInput { get; private set; }

        public UnityAction onAttack, onJump;

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
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                AttackInput = true;

                onAttack?.Invoke();
            }
            else if (context.canceled)
            {
                AttackInput = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                JumpInputPress = true;
                JumpInputRelease = false;

                onJump?.Invoke();
            }
            else if (context.canceled)
            {
                JumpInputPress = false;
                JumpInputRelease = true;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
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