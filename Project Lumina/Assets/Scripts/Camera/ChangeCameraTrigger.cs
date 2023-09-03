using Cinemachine;
using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Camera
{
    public class ChangeCameraTrigger : MonoBehaviour
    {
        [BoxGroup("Camera Settgins"), SerializeField]
        private int _cameraPriority;

        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        [FoldoutGroup("References"), SerializeField]
        private CinemachineVirtualCamera _camera;

        private void OnEnable()
        {
            _sensor.OnDetected.AddListener(ChangeCamera);
            _sensor.OnLostDetection.AddListener(ResetCamera);
        }

        private void OnDisable()
        {
            _sensor.OnDetected.RemoveListener(ChangeCamera);
            _sensor.OnLostDetection.RemoveListener(ResetCamera);
        }

        private void ChangeCamera(GameObject gameObject, Sensor sensor)
        {
            _camera.Follow = gameObject.transform;
            _camera.Priority = _cameraPriority;
        }

        private void ResetCamera(GameObject gameObject, Sensor sensor)
        {
            _camera.Follow = null;
            _camera.Priority = 0;
        }
    }
}