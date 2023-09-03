using UnityEngine;

namespace ProjectLumina.UI
{
    public class ParallaxEffect : MonoBehaviour
    {
        [Range(0, 1), SerializeField]
        private float _parallaxEffect;

        private Camera _camera;
        private float _length, _startPosition;

        private void Awake()
        {
            _camera = Camera.main;
            _startPosition = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float temp = _camera.transform.position.x * (1 - _parallaxEffect);
            float distance = _camera.transform.position.x * _parallaxEffect;

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