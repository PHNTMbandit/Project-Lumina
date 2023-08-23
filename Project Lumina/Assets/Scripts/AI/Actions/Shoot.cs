using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.Abilities;

namespace ProjectLumina.AI.Conditionals
{
    [TaskCategory("Attack")]
    public class Shoot : Action
    {
        private CharacterShoot _characterShoot;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterShoot = GetComponent<CharacterShoot>();
        }

        public override void OnStart()
        {
            base.OnStart();

            if (_characterShoot.CanShoot())
            {
                _characterShoot.UseShoot();
            }
        }
    }
}
