namespace Spells
{
    public class AkaliMota : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 45, 70, 95, 120, 145 };
        int[] effect1 = { 20, 25, 30, 35, 40 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MotaDamage = effect0[level - 1];
            float nextBuffVars_EnergyReturn = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.AkaliMota(nextBuffVars_MotaDamage, nextBuffVars_EnergyReturn), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            ApplyDamage(attacker, target, nextBuffVars_MotaDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
            float nextBuffVars_VampPercent = charVars.VampPercent; // UNUSED
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliTwinAP)) > 0)
            {
                AddBuff(owner, owner, new Buffs.AkaliShadowSwipeHealingParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class AkaliMota : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "AkaliMota",
            BuffTextureName = "AkaliMota.dds",
            NonDispellable = false,
        };
        float motaDamage;
        float energyReturn;
        EffectEmitter a;
        EffectEmitter b;
        bool doOnce;
        public AkaliMota(float motaDamage = default, float energyReturn = default)
        {
            this.motaDamage = motaDamage;
            this.energyReturn = energyReturn;
        }
        public override void OnActivate()
        {
            //RequireVar(this.motaDamage);
            //RequireVar(this.energyReturn);
            //RequireVar(this.vampPercent);
            SpellEffectCreate(out a, out _, "akali_markOftheAssasin_marker_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out b, out _, "akali_markOftheAssasin_marker_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            doOnce = true;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SpellEffectRemove(b);
        }
        public override void OnUpdateActions()
        {
            if (!doOnce)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (caster == attacker && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && doOnce)
            {
                TeamId teamID = GetTeamID_CS(attacker);
                doOnce = false;
                ApplyDamage(attacker, owner, motaDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, attacker);
                IncPAR(attacker, energyReturn, PrimaryAbilityResourceType.Energy);
                SpellEffectCreate(out _, out _, "akali_mark_impact_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AkaliTwinAP)) > 0)
                {
                    AddBuff(attacker, attacker, new Buffs.AkaliShadowSwipeHealingParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}