using Edgar.Unity;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField]
        private bool _generateOnStart;

        public void Awake()
        {
            if (_generateOnStart)
            {
                var generator = GetComponent<PlatformerGeneratorGrid2D>();
                StartCoroutine(generator.GenerateCoroutine());

                AstarPath.active.Scan();
            }
        }
    }
}