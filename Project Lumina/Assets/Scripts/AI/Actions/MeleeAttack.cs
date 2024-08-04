using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.Character;

namespace ProjectLumina.AI.Conditionals
{
    [TaskCategory("Attack")]
    public class MeleeAttack : Action
    {
        private CharacterMeleeAttack _characterMeleeAttack;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterMeleeAttack = GetComponent<CharacterMeleeAttack>();
        }

        public override void OnStart()
        {
            base.OnStart();
        }
    }
}
