using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    public abstract class CharacterAbility : MonoBehaviour
    {
        [field: ToggleLeft, SerializeField, Space]
        public bool IsUnlocked { get; private set; }

        public void LockAbility()
        {
            IsUnlocked = false;
        }

        public void UnlockAbility()
        {
            IsUnlocked = true;
        }
    }
}