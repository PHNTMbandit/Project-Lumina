using System.Collections;
using ProjectLumina.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Components
{
    [CreateAssetMenu(fileName = "New Slow Down Time Component", menuName = "Project Lumina/Neuroglyphs/Components/Slow Down Time", order = 0)]
    public class SlowDownTimeComponent : NeuroglyphComponent
    {
        [Range(0, 1), SerializeField]
        private float _slowTimeAmount;


        [Range(0, 1000), SuffixLabel("seconds"), SerializeField]
        private float _slowTimerSeconds;

        private bool _waiting = false;

        public override void Activate(GameObject target)
        {
            if (target.TryGetComponent(out CharacterRoll roll))
            {
                Stop(roll, _slowTimerSeconds);
            }
        }

        public override void Deactivate(GameObject target)
        {
            if (target.TryGetComponent(out CharacterRoll roll))
            {
                roll.StopCoroutine(Wait(_slowTimerSeconds));
            }
        }

        public void Stop(CharacterRoll roll, float duration)
        {
            if (_waiting)
            {
                return;
            }

            Time.timeScale = _slowTimeAmount;
            roll.StartCoroutine(Wait(duration));
        }

        private IEnumerator Wait(float duration)
        {
            _waiting = true;

            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;

            _waiting = false;
        }
    }
}