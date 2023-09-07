using Edgar.Unity;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    public class DungeonGenerator : MonoBehaviour
    {
        public void Awake()
        {
            var generator = GetComponent<PlatformerGeneratorGrid2D>();
            StartCoroutine(generator.GenerateCoroutine());

            AstarPath.active.Scan();
        }
    }
}