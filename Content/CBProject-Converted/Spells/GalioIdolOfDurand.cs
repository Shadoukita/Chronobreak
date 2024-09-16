namespace Spells
{
    public class GalioIdolOfDurand : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChannelDuration = 2f,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyAssistMarker(owner, target, 10);
            AddBuff(owner, target, new Buffs.GalioIdolOfDurandMarker(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            BreakSpellShields(target);
            ApplyTaunt(owner, target, 2);
        }
        public override void ChannelingStart()
        {
            AddBuff(owner, owner, new Buffs.GalioIdolOfDurand(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.GalioIdolOfDurand), owner, 0);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(owner, unit, 1, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, owner);
            }
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.GalioIdolOfDurand), owner, 0);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(owner, unit, 1, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, owner);
            }
        }
    }
}
namespace Buffs
{
    public class GalioIdolOfDurand : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "GalioIdolOfDurand",
            BuffTextureName = "Galio_IdolOfDurand.dds",
            NonDispellable = true,
        };
        float baseDamage;
        float hitCount;
        EffectEmitter areaVFXAlly;
        EffectEmitter areaVFXEnemy;
        EffectEmitter channelVFX;
        float lastTimeExecuted;
        int[] effect0 = { 220, 330, 440 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = effect0[level - 1];
            hitCount = 0;
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out areaVFXAlly, out areaVFXEnemy, "galio_beguilingStatue_taunt_indicator_team_green.troy", "galio_beguilingStatue_taunt_indicator_team_red.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out channelVFX, out _, "galio_talion_channel.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectRemove(channelVFX);
            SpellEffectRemove(areaVFXAlly);
            SpellEffectRemove(areaVFXEnemy);
            SpellEffectCreate(out _, out _, "galio_talion_breakout.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out _, out _, "galio_builingStatue_impact_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            hitCount = Math.Min(hitCount, 8);
            float bonusDmgPercent = hitCount * 0.05f;
            float totalDmgPercent = bonusDmgPercent + 1;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 575, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, totalDmgPercent, 0.6f, 1, false, false, (ObjAIBase)owner);
                SpellBuffRemove(unit, nameof(Buffs.Taunt), (ObjAIBase)owner, 0);
                SpellBuffRemove(unit, nameof(Buffs.GalioIdolOfDurandTaunt), (ObjAIBase)owner, 0);
                SpellEffectCreate(out _, out _, "galio_builingStatue_unit_impact_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, default, default, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GalioIdolOfDurandMarker), false))
                {
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.GalioIdolOfDurandMarker(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyTaunt(owner, unit, 1.5f);
                }
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.5f;
            }
            //if(damageType != DamageSource.DAMAGE_SOURCE_PERIODIC)
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC) //TODO: Verify
            {
                hitCount++;
            }
        }
    }
}