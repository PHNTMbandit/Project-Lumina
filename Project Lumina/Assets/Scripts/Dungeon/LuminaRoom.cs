using System.Collections.Generic;
using Edgar.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    public class LuminaRoom : RoomBase
    {
        [EnumPaging]
        public LuminaRoomType Type;

        public bool Outside;

        public override List<GameObject> GetRoomTemplates()
        {
            // We do not need any room templates here because they are resolved based on the type of the room.
            return null;
        }

        public override string GetDisplayName()
        {
            // Use the type of the room as its display name.
            return Type.ToString();
        }
    }
}