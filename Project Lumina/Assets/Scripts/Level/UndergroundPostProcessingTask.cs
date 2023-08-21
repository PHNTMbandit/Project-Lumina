using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using Edgar.Unity.Examples.PC2D;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectLumina.Level
{
    [CreateAssetMenu(menuName = "Project Lumina/Level/Underground/Post Processing Task", fileName = "Underground Post Processing Task")]
    public class UndergroundPostProcessingTask : DungeonGeneratorPostProcessingGrid2D
    {
        public bool SpawnEnemies;
        public GameObject[] Enemies;
        public bool CreateLevelMap;
        public TileBase WallTile;
        public TileBase LevelMapWallTile;
        public TileBase LevelMapWallBackgroundTile;
        public TileBase LevelMapBackgroundTile;
        public TileBase LevelMapPlatformTile;

        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            SetSpawnPosition(level);

            if (SpawnEnemies)
            {
                DoSpawnEnemies(level);
            }

            if (CreateLevelMap)
            {
                SetupLevelMap(level);
            }
        }

        /// <summary>
        /// Setup a schematic level map.
        /// </summary>
        private void SetupLevelMap(DungeonGeneratorLevelGrid2D level)
        {
            // Create new tilemap layer for the level map
            var tilemaps = level.GetSharedTilemaps();
            var tilemapsRoot = level.RootGameObject.transform.Find(GeneratorConstantsGrid2D.TilemapsRootName);
            var tilemapObject = new GameObject("LevelMap");
            tilemapObject.transform.SetParent(tilemapsRoot);
            tilemapObject.transform.localPosition = Vector3.zero;
            var tilemap = tilemapObject.AddComponent<Tilemap>();
            var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = 20;

            // Copy background tiles
            CopyTilesToLevelMap(level, new[] { "Background", "Other 1" }, tilemap, LevelMapBackgroundTile);

            // Copy wall background tiles
            CopyTilesToLevelMap(level, new[] { "Background" }, tilemap, LevelMapWallBackgroundTile, x => x == WallTile);

            // Copy platforms tiles
            CopyTilesToLevelMap(level, new[] { "Platforms" }, tilemap, LevelMapPlatformTile);

            // Copy wall tiles
            CopyTilesToLevelMap(level, new[] { "Walls" }, tilemap, LevelMapWallTile);
        }

        /// <summary>
        /// Spawn enemies
        /// </summary>
        /// <remarks>
        /// The method is not named "SpawnEnemies" because there is already a public field with that name.
        /// </remarks>
        private void DoSpawnEnemies(DungeonGeneratorLevelGrid2D level)
        {
            // Check that we have at least one enemy to choose from
            if (Enemies == null || Enemies.Length == 0)
            {
                throw new InvalidOperationException("There must be at least one enemy prefab to spawn enemies");
            }

            // Go through individual rooms
            foreach (var roomInstance in level.RoomInstances)
            {
                var roomTemplate = roomInstance.RoomTemplateInstance;

                // Find the game object that holds all the spawn points
                var enemySpawnPoints = roomTemplate.transform.Find("EnemySpawnPoints");

                if (enemySpawnPoints != null)
                {
                    // Go through individual spawn points and choose a random enemy to spawn
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

        /// <summary>
        /// Move the player to the spawn point of the level.
        /// </summary>
        /// <param name="level"></param>
        private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
        {
            // Find the room with the Entrance type
            var entranceRoomInstance = level
                .RoomInstances
                .FirstOrDefault(x => ((LuminaRoom)x.Room).Type == LuminaRoomType.Entrance);

            if (entranceRoomInstance == null)
            {
                throw new InvalidOperationException("Could not find Entrance room");
            }

            var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;

            // Find the spawn position marker
            var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");

            // Move the player to the spawn position
            var player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPosition.position;
        }

        /// <summary>
        /// Copy tiles from given source tilemaps to the level map tilemap.
        /// Instead of using the original tiles, we use a given level map tile (which is usually only a single color).
        /// If we want to copy only some of the tiles, we can provide a tile filter function.
        /// </summary>
        private void CopyTilesToLevelMap(DungeonGeneratorLevelGrid2D level, ICollection<string> sourceTilemapNames, Tilemap levelMapTilemap, TileBase levelMapTile, Predicate<TileBase> tileFilter = null)
        {
            // Go through the tilemaps with the correct name
            foreach (var sourceTilemap in level.GetSharedTilemaps().Where(x => sourceTilemapNames.Contains(x.name)))
            {
                // Go through positions inside the bounds of the tilemap
                foreach (var tilemapPosition in sourceTilemap.cellBounds.allPositionsWithin)
                {
                    // Check if there is a tile at a given position
                    var originalTile = sourceTilemap.GetTile(tilemapPosition);

                    if (originalTile != null)
                    {
                        // If a tile filter is provided, use it to check if the predicate holds
                        if (tileFilter != null)
                        {
                            if (tileFilter(originalTile))
                            {
                                levelMapTilemap.SetTile(tilemapPosition, levelMapTile);
                            }
                        }
                        // Otherwise set the levelMapTile to the correct position
                        else
                        {
                            levelMapTilemap.SetTile(tilemapPosition, levelMapTile);
                        }
                    }
                }
            }
        }
    }
}