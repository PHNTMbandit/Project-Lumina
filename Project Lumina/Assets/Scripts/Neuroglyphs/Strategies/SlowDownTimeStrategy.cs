using System.Collections;
using ProjectLumina.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs.Strategies
{
    public class SlowDownTimeStrategy : INeuroglyphStrategy
    {
        [Range(0, 1), SerializeField]
        private float _slowTimeAmount;


        [Range(0, 1000), SuffixLabel("seconds"), SerializeField]
        private float _slowTimerSeconds;

        private bool _waiting = false;

        public void Activate(GameObject target)
        {
            if (target.TryGetComponent(out CharacterRoll roll))
            {
                Stop(roll, _slowTimerSeconds);
            }
        }

        public void Deactivate(GameObject target)
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