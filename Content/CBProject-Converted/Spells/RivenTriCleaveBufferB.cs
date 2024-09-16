namespace Spells
{
    public class RivenTriCleaveBufferB : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            bool nextBuffVars_ChampionLock = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, targetPos, 125, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                targetPos = GetUnitPosition(unit);
                nextBuffVars_ChampionLock = true;
                AddBuff(owner, unit, new Buffs.RivenTriCleaveBufferLock(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            Vector3 nextBuffVars_TargetPos = targetPos;
            int nextBuffVars_Level = level;
            AddBuff(owner, owner, new Buffs.RivenTriCleaveBuffered(nextBuffVars_ChampionLock, nextBuffVars_TargetPos, nextBuffVars_Level), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RivenTriCleaveBufferB : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenTriCleaveBuffer));
            SetSlotSpellCooldownTimeVer2(0.1f, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenTriCleave));
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenTriCleaveBuffered)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.RivenTriCleaveBuffered));
            }
        }
    }
}