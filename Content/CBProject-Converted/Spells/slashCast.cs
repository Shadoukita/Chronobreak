namespace Spells
{
    public class slashCast : SlashCast {}
    public class SlashCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "TryndamereDemonsword", },
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                bool canMove = GetCanMove(owner);
                if (!canMove)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 650)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 645, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class SlashCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
    }
}