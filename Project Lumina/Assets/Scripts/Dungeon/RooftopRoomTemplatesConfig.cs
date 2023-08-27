using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Dungeon
{
    [Serializable, HideLabel]
    public class RooftopRoomTemplatesConfig
    {
        public GameObject[] EntranceRoomTemplates;
        public GameObject[] ExitRoomTemplates;
        public GameObject[] OutsideNormalRoomTemplates;
        public GameObject[] OutsideTeleportRoomTemplates;
        public GameObject[] InsideTreasureRoomTemplates;
        public GameObject[] InsideTeleportRoomTemplates;
        public GameObject[] InsideNormalRoomTemplates;
        public GameObject[] InsideShopRoomTemplates;
        public GameObject[] InsideCorridorRoomTemplates;

        public GameObject[] GetRoomTemplates(LuminaRoom room)
        {
            if (room.Outside)
            {
                switch (room.Type)
                {
                    case LuminaRoomType.Entrance:
                        return EntranceRoomTemplates;

                    case LuminaRoomType.Exit:
                        return ExitRoomTemplates;

                    case LuminaRoomType.Teleport:
                        return OutsideTeleportRoomTemplates;

                    case LuminaRoomType.Normal:
                        return OutsideNormalRoomTemplates;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (room.Type)
                {
                    case LuminaRoomType.Teleport:
                        return InsideTeleportRoomTemplates;

                    case LuminaRoomType.Treasure:
                        return InsideTreasureRoomTemplates;

                    case LuminaRoomType.Shop:
                        return InsideShopRoomTemplates;

                    case LuminaRoomType.Normal:
                        return InsideNormalRoomTemplates;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}