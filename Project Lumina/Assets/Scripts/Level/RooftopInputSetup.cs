using System.Linq;
using Edgar.Unity;
using UnityEngine;

namespace ProjectLumina.Level
{
    [CreateAssetMenu(menuName = "Project Lumina/Level/Rooftop/Input Setup", fileName = "Rooftop Input Setup")]
    public class RooftopInputSetup : DungeonGeneratorInputBaseGrid2D
    {
        public LevelGraph LevelGraph;
        public RooftopRoomTemplatesConfig RoomTemplates;

        protected override LevelDescriptionGrid2D GetLevelDescription()
        {
            var levelDescription = new LevelDescriptionGrid2D();

            foreach (var room in LevelGraph.Rooms.Cast<LuminaRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            foreach (var connection in LevelGraph.Connections.Cast<LuminaConnection>())
            {
                var from = (LuminaRoom)connection.From;
                var to = (LuminaRoom)connection.To;

                if (from.Outside && to.Outside)
                {
                    levelDescription.AddConnection(connection);
                }
                else
                {
                    var corridorRoom = CreateInstance<LuminaRoom>();
                    corridorRoom.Type = LuminaRoomType.Corridor;

                    levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.InsideCorridorRoomTemplates.ToList());
                }
            }

            return levelDescription;
        }
    }
}