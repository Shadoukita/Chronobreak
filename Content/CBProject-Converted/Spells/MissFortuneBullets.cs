namespace Spells
{
    public class MissFortuneBullets : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 65, 95, 125, 185, 230 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromAll(target, nameof(Buffs.MissfortuneBulletHolder));
            if (count <= 7)
            {
                int count1 = GetBuffCountFromAll(target, nameof(Buffs.MissFortuneWaveHold));
                if (count1 < 1)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    AddBuff(attacker, target, new Buffs.MissFortuneWaveHold(), 2, 1, 0.05f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.MissfortuneBulletHolder(), 9, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    float baseDamage = effect0[level - 1];
                    float totalDamage = GetTotalAttackDamage(owner);
                    float baseAtkDmg = GetBaseAttackDamage(owner);
                    float bonusDamage = totalDamage - baseAtkDmg;
                    bonusDamage *= 0.45f;
                    float aPPreMod = GetFlatMagicDamageMod(owner);
                    float aPPostMod = 0.2f * aPPreMod;
                    float aDAPBonus = bonusDamage + aPPostMod;
                    float finalDamage = baseDamage + aDAPBonus;
                    ApplyDamage(owner, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    SpellEffectCreate(out _, out _, "missFortune_bulletTime_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class MissFortuneBullets : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
    }
}