namespace Spells
{
    public class SummonerTestForceSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 3000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                int level = GetSlotSpellLevel((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 0)
                {
                    IncSpellLevel((ObjAIBase)unit, 0, SpellSlotType.SpellSlots);
                }
                SpellCast((ObjAIBase)unit, owner, default, default, 0, SpellSlotType.SpellSlots, 0, true, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerTestForceSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Cleanse",
            BuffTextureName = "Summoner_boost.dds",
        };
    }
}