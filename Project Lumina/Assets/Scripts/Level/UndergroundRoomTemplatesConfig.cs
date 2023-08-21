using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Level
{
    [Serializable, HideLabel]
    public class UndergroundRoomTemplatesConfig
    {
        public GameObject[] DefaultRoomTemplates;
        public GameObject[] ShopRoomTemplates;
        public GameObject[] TeleportRoomTemplates;
        public GameObject[] TreasureRoomTemplates;
        public GameObject[] EntranceRoomTemplates;
        public GameObject[] ExitRoomTemplates;
        public GameObject[] CorridorRoomTemplates;

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