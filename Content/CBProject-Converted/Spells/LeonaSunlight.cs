namespace Buffs
{
    public class LeonaSunlight : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "LeonaSunlight",
            BuffTextureName = "LeonaSunlight.dds",
        };
        EffectEmitter particle1;
        ObjAIBase attacker1; // UNUSED
        TeamId teamIDAttacker; // UNUSED
        int[] effect0 = { 20, 20, 35, 35, 50, 50, 65, 65, 80, 80, 95, 95, 110, 110, 125, 125, 140, 140 };
        public override void OnActivate()
        {
            if (owner is Champion)
            {
                SpellEffectCreate(out particle1, out _, "Leona_Sunlight_Champion.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out particle1, out _, "Leona_Sunlight.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (attacker is Champion)
            {
                ObjAIBase caster = GetBuffCasterUnit();
                if (caster != attacker)
                {
                    TeamId teamIDAttacker = GetTeamID_CS(attacker);
                    TeamId teamIDCaster = GetTeamID_CS(caster);
                    if (teamIDAttacker == teamIDCaster)
                    {
                        int level = GetLevel(caster);
                        float sunlightDamage = effect0[level - 1];
                        bool sunglasses = TestUnitAttributeFlag(owner, ExtraAttributeFlag.HAS_SUNGLASSES);
                        if (sunglasses)
                        {
                            sunlightDamage--;
                        }
                        attacker1 = attacker;
                        this.teamIDAttacker = teamIDAttacker;
                        SpellEffectCreate(out _, out _, "LeonaPassive_tar.troy", default, teamIDAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        SpellBuffClear(owner, nameof(Buffs.LeonaSunlight));
                        ApplyDamage(attacker, owner, sunlightDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}