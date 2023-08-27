using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    [Serializable, HideLabel]
    public class UndergroundRoomTemplatesConfig
    {
        public GameObject[] CorridorRoomTemplates;
        public GameObject[] DefaultRoomTemplates;
        public GameObject[] EntranceRoomTemplates;
        public GameObject[] ExitRoomTemplates;
        public GameObject[] ShopRoomTemplates;
        public GameObject[] TeleportRoomTemplates;
        public GameObject[] TreasureRoomTemplates;

        public GameObject[] GetRoomTemplates(LuminaRoom room)
        {
            switch (room.Type)
            {
                case LuminaRoomType.Shop:
                    return ShopRoomTemplates;

                case LuminaRoomType.Teleport:
                    return TeleportRoomTemplates;

                case LuminaRoomType.Treasure:
                    return TreasureRoomTemplates;

                case LuminaRoomType.Entrance:
                    return EntranceRoomTemplates;

                case LuminaRoomType.Exit:
                    return ExitRoomTemplates;

                default:
                    return DefaultRoomTemplates;
            }
        }
    }
}