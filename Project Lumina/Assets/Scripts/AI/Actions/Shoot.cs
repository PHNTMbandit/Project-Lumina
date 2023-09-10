using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.Character;
using UnityEngine;

namespace ProjectLumina.AI.Conditionals
{
    [TaskCategory("Attack")]
    public class Shoot : Action
    {
        private CharacterShoot _characterShoot;
        private bool _isShooting;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterShoot = GetComponent<CharacterShoot>();
            _characterShoot.onShootFinished += delegate { _isShooting = true; };
        }

        public override TaskStatus OnUpdate()
        {
            if (_isShooting == false)
            {
                if (_characterShoot.CanShoot())
                {
                    _characterShoot.UseShoot();
                }

                return TaskStatus.Running;
            }
            else
            {
                _isShooting = false;

                return TaskStatus.Success;
            }
        }
    }
}
