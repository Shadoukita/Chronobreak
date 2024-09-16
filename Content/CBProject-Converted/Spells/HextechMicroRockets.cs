namespace Spells
{
    public class HeimerdingerW: HextechMicroRockets {}
    public class HextechMicroRockets : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Region bubbleID; // UNUSED
        public override bool CanCast()
        {
            foreach (AttackableUnit unit in GetRandomUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                if (CanSeeTarget(owner, unit))
                {
                    return true;
                }
            }
            return false;
        }
        public override void SelfExecute()
        {
            bool result;
            TeamId casterID = GetTeamID_CS(attacker);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 5, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
            else
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 3, default, true))
                {
                    result = CanSeeTarget(owner, unit);
                    if (result)
                    {
                        bubbleID = AddUnitPerceptionBubble(casterID, 300, unit, 1, default, default, false);
                        SpellCast(owner, unit, owner.Position3D, owner.Position3D, 1, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class HextechMicroRockets : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", },
            AutoBuffActivateEffect = new[] { "rapidfire_buf.troy", },
            BuffName = "",
            BuffTextureName = "Tristana_headshot.dds",
        };
    }
}