using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [AddComponentMenu("Capabilities/Seekable")]
    public class Seekable : MonoBehaviour
    {
        [Range(0, 10), SerializeField]
        private float _speed;

        [SerializeField]
        protected RangeSensor2D sensor;

        [Space]
        public UnityEvent OnSeeked;

        private Transform _target;
        private bool _isFollowing = false;

        private void OnEnable()
        {
            sensor.OnDetected.AddListener(CheckDetection);
        }

        private void OnDisable()
        {
            _isFollowing = false;
            _target = null;
            sensor.OnDetected.RemoveListener(CheckDetection);
        }

        private void Update()
        {
            if (_isFollowing)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            }
        }

        private void CheckDetection(GameObject gameObject, Sensor sensor)
        {
            _isFollowing = true;
            _target = gameObject.transform;

            OnSeeked?.Invoke();
        }
    }
}