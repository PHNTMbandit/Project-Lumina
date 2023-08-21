using System.Linq;
using ProjectLumina.Level;
using UnityEngine;

namespace Edgar.Unity.Examples.Metroidvania
{
    [CreateAssetMenu(menuName = "Project Lumina/Level/Underground/Input Setup", fileName = "Underground Input Setup")]
    public class UndergroundInputSetupTask : DungeonGeneratorInputBaseGrid2D
    {
        public LevelGraph LevelGraph;
        public UndergroundRoomTemplatesConfig RoomTemplates;

        /// <summary>
        /// This is the main method of the input setup.
        /// It prepares the description of the level for the procedural generator.
        /// </summary>
        /// <returns></returns>
        protected override LevelDescriptionGrid2D GetLevelDescription()
        {
            var levelDescription = new LevelDescriptionGrid2D();

            // Go through individual rooms and add each room to the level description
            // Room templates are resolved based on their type
            foreach (var room in LevelGraph.Rooms.Cast<LuminaRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            // Go through individual connections and for each connection create a corridor room
            foreach (var connection in LevelGraph.Connections.Cast<LuminaConnection>())
            {
                var corridorRoom = CreateInstance<LuminaRoom>();
                corridorRoom.Type = LuminaRoomType.Corridor;
                levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.CorridorRoomTemplates.ToList());
            }

            return levelDescription;
        }
    }
}