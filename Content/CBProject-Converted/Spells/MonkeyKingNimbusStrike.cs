namespace Spells
{
    public class MonkeyKingNimbusStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
        float[] effect0 = { 0.01f, 0.01f, 0.01f, 0.01f, 0.01f };
        int[] effect1 = { 7, 14, 21, 28, 35 };
        float[] effect2 = { 0.09f, 0.12f, 0.15f, 0.18f, 0.21f };
        public override void SelfExecute()
        {
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float healthPercent = effect0[level - 1];
            float baseDamage = effect1[level - 1];
            float nextBuffVars_LifestealStat = effect2[level - 1];
            float healthDamage = healthPercent * maxHealth;
            float nextBuffVars_DamageGain = healthDamage + baseDamage;
            AddBuff(owner, owner, new Buffs.MonkeyKingNimbusStrike(nextBuffVars_DamageGain, nextBuffVars_LifestealStat), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MonkeyKingNimbusStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", "BUFFBONE_CSTM_WEAPON_4", "BUFFBONE_CSTM_WEAPON_2", },
            AutoBuffActivateEffect = new[] { "olaf_viciousStrikes_self.troy", "olaf_viciousStrikes_self.troy", "olaf_viciousStrikes_axes_blood.troy", "olaf_viciousStrikes_axes_blood.troy", },
            BuffName = "OlafFrenziedStrikes",
            BuffTextureName = "OlafViciousStrikes.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        EffectEmitter particleID; // UNUSED
        float damageGain;
        float lifestealStat;
        public MonkeyKingNimbusStrike(float damageGain = default, float lifestealStat = default)
        {
            this.damageGain = damageGain;
            this.lifestealStat = lifestealStat;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out particleID, out _, "olaf_viciousStrikes_weapon_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_3", default, owner, "BUFFBONE_CSTM_WEAPON_2", default, false);
            SpellEffectCreate(out particleID, out _, "olaf_viciousStrikes_weapon_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, owner, "BUFFBONE_CSTM_WEAPON_7", default, owner, "BUFFBONE_CSTM_WEAPON_4", default, false);
            //RequireVar(this.damageGain);
            //RequireVar(this.lifestealStat);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageGain);
            IncPercentLifeStealMod(owner, lifestealStat);
            IncPercentSpellVampMod(owner, lifestealStat);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            SpellEffectCreate(out _, out _, "olaf_viciousStrikes_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
    }
}