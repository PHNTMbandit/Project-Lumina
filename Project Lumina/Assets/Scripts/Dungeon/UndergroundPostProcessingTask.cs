using System;
using System.Linq;
using Edgar.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    [CreateAssetMenu(menuName = "Project Lumina/Level/Underground/Post Processing Task", fileName = "Underground Post Processing Task")]
    public class UndergroundPostProcessingTask : DungeonGeneratorPostProcessingGrid2D
    {
        public bool SpawnEnemies;

        [ShowIf("SpawnEnemies")]
        public GameObject[] Enemies;

        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            SetSpawnPosition(level);

            if (SpawnEnemies)
            {
                DoSpawnEnemies(level);
            }
        }

        private void DoSpawnEnemies(DungeonGeneratorLevelGrid2D level)
        {
            if (Enemies == null || Enemies.Length == 0)
            {
                throw new InvalidOperationException("There must be at least one enemy prefab to spawn enemies");
            }

            foreach (var roomInstance in level.RoomInstances)
            {
                var roomTemplate = roomInstance.RoomTemplateInstance;
                var enemySpawnPoints = roomTemplate.transform.Find("Enemy Spawn Points");

                if (enemySpawnPoints != null)
                {
                    foreach (Transform enemySpawnPoint in enemySpawnPoints)
                    {
                        var enemyPrefab = Enemies[Random.Next(Enemies.Length)];
                        var enemy = Instantiate(enemyPrefab);
                        enemy.transform.parent = roomTemplate.transform;
                        enemy.transform.position = enemySpawnPoint.position;
                    }
                }
            }
        }

        private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
        {
            var entranceRoomInstance = level
                .RoomInstances
                .FirstOrDefault(x => ((LuminaRoom)x.Room).Type == LuminaRoomType.Entrance) ?? throw new InvalidOperationException("Could not find Entrance room");

            var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;
            var spawnPosition = roomTemplateInstance.transform.Find("Spawn Point");
            var player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPosition.position;
        }
    }
}