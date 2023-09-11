using System.Collections;
using UnityEngine;

namespace ProjectLumina.Effects
{
    public class HitStop : MonoBehaviour
    {
        private bool _waiting = false;

        public void Stop(float duration, float timeScale)
        {
            if (_waiting)
            {
                return;
            }

            Time.timeScale = timeScale;
            StartCoroutine(Wait(duration));
        }

        public void Stop(float duration)
        {
            Stop(duration, 0.0f);
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