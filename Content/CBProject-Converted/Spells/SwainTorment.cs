namespace Spells
{
    public class SwainTorment : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 18.75f, 28.75f, 38.75f, 48.75f, 58.75f };
        float[] effect1 = { 1.08f, 1.11f, 1.14f, 1.17f, 1.2f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DoTDamage = effect0[level - 1];
            float nextBuffVars_SwainMultiplier = effect1[level - 1]; // UNUSED
            AddBuff(attacker, target, new Buffs.SwainTorment(nextBuffVars_DoTDamage), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class SwainTorment : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainTormentDoT",
            BuffTextureName = "SwainTorment.dds",
        };
        float doTDamage;
        EffectEmitter swainTormentEffect;
        EffectEmitter swainDoTEffect;
        EffectEmitter swainDoTEffect2;
        int damageTaken; // UNUSED
        float lastTimeExecuted;
        public SwainTorment(float doTDamage = default)
        {
            this.doTDamage = doTDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmpPerc);
            //RequireVar(this.doTDamage);
            //RequireVar(this.swainMultiplier);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            SpellEffectCreate(out swainTormentEffect, out _, "swain_torment_tar.troy", default, TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            SpellEffectCreate(out swainDoTEffect, out _, "swain_torment_marker.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectCreate(out swainDoTEffect2, out _, "swain_torment_dot.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            damageTaken = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(swainTormentEffect);
            SpellEffectRemove(swainDoTEffect);
            SpellEffectRemove(swainDoTEffect2);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, doTDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.2f, 1, false, false, attacker);
            }
        }
    }
}