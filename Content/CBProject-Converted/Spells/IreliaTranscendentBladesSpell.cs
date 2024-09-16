namespace Spells
{
    public class IreliaTranscendentBladesSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 80, 120, 160 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_DrainPercent;
            BreakSpellShields(target);
            float baseDamage = effect0[level - 1];
            float physPreMod = GetFlatPhysicalDamageMod(owner);
            float physPostMod = 0.6f * physPreMod;
            float aPPreMod = GetFlatMagicDamageMod(owner);
            float aPPostMod = 0.5f * aPPreMod;
            TeamId ireliaTeamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "irelia_ult_tar.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, "root", default, target, default, default, true, default, default, false, false);
            float damageToDeal1 = physPostMod + baseDamage;
            float damageToDeal2 = aPPostMod + damageToDeal1;
            if (target is Champion)
            {
                nextBuffVars_DrainPercent = 0.25f;
            }
            else
            {
                nextBuffVars_DrainPercent = 0.1f;
            }
            AddBuff(owner, owner, new Buffs.GlobalDrain(nextBuffVars_DrainPercent), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage(attacker, target, damageToDeal2, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class IreliaTranscendentBladesSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "IreliaTranscendentBlades",
            BuffTextureName = "Irelia_TranscendentBladesReady.dds",
        };
    }
}