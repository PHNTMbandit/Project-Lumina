using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectLumina.Assets.Scripts.Utilities
{
    public class Timer : MonoBehaviour
    {
        private static Timer _instance;
        private List<Action> _actions = new();

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void CallAfterDelay(Action action, float delay)
        {
            if (_instance == null)
            {
                GameObject timerObject = new("Timer");
                _instance = timerObject.AddComponent<Timer>();
            }

            _instance.StartCoroutine(_instance.ExecuteAfterDelay(action, delay));
        }

        private IEnumerator ExecuteAfterDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}
