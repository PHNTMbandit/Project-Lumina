using UnityEngine;

namespace ProjectLumina.Camera
{
    public class Parallax : MonoBehaviour
    {
        [Range(0, 1), SerializeField]
        private float _parallaxEffect;

        private float _length, _startPosition;
        private Transform _cameraTransform;

        private void Awake()
        {
            _startPosition = transform.position.x;
            _cameraTransform = UnityEngine.Camera.main.transform;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float temp = _cameraTransform.position.x * (1 - _parallaxEffect);
            float distance = _cameraTransform.position.x * _parallaxEffect;

            transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

            if (temp > _startPosition + _length)
            {
                _startPosition += _length;
            }
            else if (temp < _startPosition - _length)
            {
                _startPosition -= _length;
            }
        }
    }
}