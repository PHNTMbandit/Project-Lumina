using UnityEngine;

namespace ProjectLumina.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Player/Player Movement")]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 25)]
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(float move)
        {
            _rigidbody.velocity = new Vector2(move * _moveSpeed, _rigidbody.velocity.y);
        }
    }
}