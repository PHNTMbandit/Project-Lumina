using ProjectLumina.Character;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(fileName = "New Move Speed Component", menuName = "Project Lumina/Neuroglyphs/Components/Move Speed", order = 0)]
    public class MoveSpeedComponent : NeuroglyphComponent
    {
        [Range(-100, 100), SuffixLabel("%"), SerializeField]
        private float _moveSpeedModifier;

        public override void Activate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.MoveSpeed.AddModifier(new PercentageStatModifier(_moveSpeedModifier));
            }
        }

        public override void Deactivate(GameObject user)
        {
            if (user.TryGetComponent(out CharacterMove characterMove))
            {
                characterMove.MoveSpeed.ClearModifiers();
            }
        }
    }
}