namespace Spells
{
    public class AhriTumbleMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 100, 140, 180, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (charVars.TumbleIsActive == 1)
            {
                TeamId teamID; // UNITIALIZED
                teamID = TeamId.TEAM_UNKNOWN; //TODO: Verify
                SpellEffectCreate(out _, out _, "Ahri_PassiveHeal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "Ahri_passive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.AhriSoulCrusher2(), 1, 1, 0.5f, BuffAddType.STACKS_AND_CONTINUE, BuffType.INTERNAL, 0, true, false, false);
                float nextBuffVars_DrainPercent = 0.35f;
                bool nextBuffVars_DrainedBool = false;
                AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) > 0)
                {
                    SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.AhriSoulCrusher), 1);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.AhriSoulCrusher)) == 0)
                {
                    AddBuff(attacker, attacker, new Buffs.AhriSoulCrusherCounter(), 9, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.3f, 0, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class AhriTumbleMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
    }
}