using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsRollAttacking { get; private set; }

        [BoxGroup("Damage"), Range(0, 100), SerializeField]
        private float _damage;

        public void RollAttack()
        {
            IsRollAttacking = true;
        }

        public void FinishRollAttack()
        {
            IsRollAttacking = false;
        }
    }
}