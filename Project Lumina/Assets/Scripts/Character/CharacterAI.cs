using Pathfinding;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/Character AI")]
    public class CharacterAI : MonoBehaviour
    {
        public GameObject Target { get; private set; }
        public float MoveSpeed { get; private set; }

        private AIPath _AIPath;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _AIPath = GetComponent<AIPath>();
            _characterMove = GetComponent<CharacterMove>();

            _AIPath.onSearchPath += SetFacingDirection;
        }

        private void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");

            MoveSpeed = _characterMove.MoveSpeed.Value;
            _AIPath.maxSpeed = MoveSpeed;
            _AIPath.maxAcceleration = _characterMove.MoveSpeed.Value;
        }

        private void SetFacingDirection()
        {
            Vector3 direction = _AIPath.steeringTarget - transform.position;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}