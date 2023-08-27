using System.Linq;
using Edgar.Unity;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    [CreateAssetMenu(menuName = "Project Lumina/Level/Underground/Input Setup", fileName = "Underground Input Setup")]
    public class UndergroundInputSetupTask : DungeonGeneratorInputBaseGrid2D
    {
        public LevelGraph LevelGraph;
        public UndergroundRoomTemplatesConfig RoomTemplates;

        protected override LevelDescriptionGrid2D GetLevelDescription()
        {
            var levelDescription = new LevelDescriptionGrid2D();

            foreach (var room in LevelGraph.Rooms.Cast<LuminaRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

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