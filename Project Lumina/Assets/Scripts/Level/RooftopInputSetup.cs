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

            // Go through individual rooms and add each room to the level description
            foreach (var room in LevelGraph.Rooms.Cast<LuminaRoom>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            foreach (var connection in LevelGraph.Connections.Cast<LuminaConnection>())
            {
                var from = (LuminaRoom)connection.From;
                var to = (LuminaRoom)connection.To;

                // If both rooms are outside, we do not need a corridor room
                if (from.Outside && to.Outside)
                {
                    levelDescription.AddConnection(connection);
                }
                // If at least one room is inside, we need a corridor room to properly connect the two rooms
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