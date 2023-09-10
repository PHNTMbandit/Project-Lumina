using ProjectLumina.Character;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Strategies
{
    public class MoveSpeedStrategy : INeuroglyphStrategy
    {
        [Range(-100, 100), SuffixLabel("%"), SerializeField]
        private float _moveSpeedModifier;

        public void Activate(GameObject target)
        {
            if (target.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.MoveSpeed.AddModifier(new PercentageStatModifier(_moveSpeedModifier));
            }
        }

        public void Deactivate(GameObject target)
        {
            if (target.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.MoveSpeed.ClearModifiers();
            }
        }
    }
}