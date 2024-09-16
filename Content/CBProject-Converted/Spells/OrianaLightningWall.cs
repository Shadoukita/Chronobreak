namespace Spells
{
    public class OrianaLightningWall : SpellScript
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
    public class OrianaLightningWall : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        EffectEmitter particle;
        float tickDamage;
        float lastTimeExecuted;
        float[] effect0 = { 20, 27.5f, 35 };
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out _, "ManaLeach_tar2.troy", default, teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetTargetable(owner, false);
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float selfAP = GetFlatMagicDamageMod(caster);
            float bonusDamage = selfAP * 0.25f;
            float totalDamage = baseDamage + bonusDamage;
            tickDamage = totalDamage;
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            SpellEffectRemove(particle);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 120, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_TickDamage = tickDamage;
                    AddBuff(attacker, unit, new Buffs.OrianaDoT(nextBuffVars_TickDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                }
            }
        }
    }
}