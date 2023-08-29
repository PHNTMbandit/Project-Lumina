using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProjectLumina.Dungeon
{
    [CreateAssetMenu(menuName = "Project Lumina/Dungeon/Autotiling Tile", fileName = "New Autotiling Tile")]
    public class AutotilingTile : RuleTile
    {
        public bool applySiblingAutotiling;

        public enum SiblingGroup
        {
            Group1,
            Group2,
            Group3,
            Group4,
            Group5,
        }

        public SiblingGroup siblingGroup;

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            if (applySiblingAutotiling)
            {
                if (other is RuleOverrideTile)
                {
                    other = (other as RuleOverrideTile).m_InstanceTile;
                }

                switch (neighbor)
                {
                    case TilingRuleOutput.Neighbor.This:
                        {
                            return other is AutotilingTile && (other as AutotilingTile).siblingGroup == siblingGroup;
                        }
                    case TilingRuleOutput.Neighbor.NotThis:
                        {
                            return !(other is AutotilingTile && (other as AutotilingTile).siblingGroup == siblingGroup);
                        }
                }
            }

            return base.RuleMatch(neighbor, other);
        }
    }
}