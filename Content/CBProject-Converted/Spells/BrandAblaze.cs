namespace Spells
{
    public class BrandAblaze : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BrandAblaze : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "BrandAblaze",
            BuffTextureName = "BrandBlaze.dds",
            IsDeathRecapSource = true,
        };
        EffectEmitter a;
        EffectEmitter b;
        EffectEmitter c;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            int brandSkinID = GetSkinID(attacker);
            TeamId teamID = GetTeamID_CS(attacker);
            if (brandSkinID == 3)
            {
                SpellEffectCreate(out a, out _, "BrandBlaze_hotfoot_Frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out b, out _, "BrandBlaze_hotfoot_Frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out c, out _, "BrandFireMark_Frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "BrandBlaze_hotfoot.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out b, out _, "BrandBlaze_hotfoot.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_BUFFBONE_GLB_FOOT_LOC", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out c, out _, "BrandFireMark.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SpellEffectRemove(b);
            SpellEffectRemove(c);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float damageToDeal = maxHealth * 0.02f;
            TeamId teamID = GetTeamID_CS(owner);
            if (teamID == TeamId.TEAM_NEUTRAL)
            {
                damageToDeal = Math.Min(damageToDeal, 80);
            }
            ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1.05f, ref lastTimeExecuted, false))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float damageToDeal = maxHealth * 0.02f;
                TeamId teamID = GetTeamID_CS(owner);
                if (teamID == TeamId.TEAM_NEUTRAL)
                {
                    damageToDeal = Math.Min(damageToDeal, 80);
                }
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}