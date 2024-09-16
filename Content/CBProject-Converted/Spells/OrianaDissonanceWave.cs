namespace Spells
{
    public class OrianaDissonanceWave : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class OrianaDissonanceWave : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        EffectEmitter particle2;
        EffectEmitter particle;
        Vector3 targetPos;
        float lastTimeExecuted;
        public OrianaDissonanceWave(EffectEmitter particle2 = default, EffectEmitter particle = default, Vector3 targetPos = default)
        {
            this.particle2 = particle2;
            this.particle = particle;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.particle);
            //RequireVar(this.particle2);
            TeamId teamOfOwner = GetTeamID_CS(owner); // UNUSED
            Vector3 targetPos = this.targetPos; // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos = this.targetPos;
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                int nextBuffVars_Level;
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaShock), false))
                {
                    nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff(attacker, unit, new Buffs.OrianaSlow(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 225, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaShock), false))
                {
                    nextBuffVars_Level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    AddBuff(attacker, unit, new Buffs.OrianaHaste(nextBuffVars_Level), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}