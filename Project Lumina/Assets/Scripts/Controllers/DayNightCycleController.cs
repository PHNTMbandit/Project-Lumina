using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace ProjectLumina.Controllers
{
    public class DayNightCycleController : MonoBehaviour
    {
        public DateTime CurrentTime { get; private set; }

        [BoxGroup("Time"), SerializeField, Range(0, 23)]
        private int startHour;

        [BoxGroup("Time"), SerializeField, Range(0, 1)]
        private float _timeRate;

        [BoxGroup("Lighting"), SerializeField]
        private AnimationCurve _cycleCurve;

        [BoxGroup("Lighting"), SerializeField]
        private Light2D _globalLight;

        private void Start()
        {
            CurrentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

            StartCoroutine(UpdateTime());
        }

        private IEnumerator UpdateTime()
        {
            while (true)
            {
                CurrentTime = CurrentTime.AddMinutes(1);

                _globalLight.intensity = _cycleCurve.Evaluate((float)CurrentTime.TimeOfDay.TotalMinutes);

                yield return new WaitForSeconds(_timeRate);
            }
        }

        public TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
        {
            TimeSpan difference = toTime - fromTime;

            if (difference.TotalSeconds < 0)
            {
                difference += TimeSpan.FromHours(24);
            }

            return difference;
        }
    }
}