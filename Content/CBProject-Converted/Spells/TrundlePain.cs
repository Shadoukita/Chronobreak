namespace Spells
{
    public class TrundlePain : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        int[] effect0 = { 100, 175, 250 };
        float[] effect1 = { 0.15f, 0.2f, 0.25f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            BreakSpellShields(target);
            float nextBuffVars_DamageDealt = effect0[level - 1];
            float nextBuffVars_Survivability = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.TrundlePain(nextBuffVars_DamageDealt), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.TrundlePainShred(nextBuffVars_Survivability), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TrundlePain : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "TrundlePain",
            BuffTextureName = "Trundle_Agony.dds",
        };
        float damageDealt;
        float moddedDamage;
        float damageSecond;
        EffectEmitter asdf;
        float lastTimeExecuted;
        public TrundlePain(float damageDealt = default)
        {
            this.damageDealt = damageDealt;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageDealt);
            float aPStat = GetFlatMagicDamageMod(attacker);
            float aPRatio = aPStat * 0.6f;
            moddedDamage = aPRatio + damageDealt;
            damageSecond = moddedDamage / 6;
            ApplyDamage(attacker, owner, moddedDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            IncHealth(attacker, moddedDamage, attacker);
            SpellEffectCreate(out asdf, out _, "TrundleUltParticle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, damageSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                IncHealth(attacker, damageSecond, attacker);
            }
        }
    }
}