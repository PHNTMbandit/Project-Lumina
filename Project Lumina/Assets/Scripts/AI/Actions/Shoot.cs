using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.Abilities;
using UnityEngine;

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

        public override TaskStatus OnUpdate()
        {
            base.OnUpdate();

            _characterShoot.UseShoot();

            return TaskStatus.Success;
        }
    }
}
