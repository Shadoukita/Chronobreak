namespace Spells
{
    public class HungeringStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellVOOverrideSkins = new[] { "HyenaWarwick", },
        };
        float[] effect0 = { 0.08f, 0.11f, 0.14f, 0.17f, 0.2f };
        int[] effect1 = { 75, 125, 175, 225, 275 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int warwickSkinID = GetSkinID(attacker);
            if (warwickSkinID == 6)
            {
                SpellEffectCreate(out _, out _, "HungeringStrikeFire_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "HungeringStrike_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            float nextBuffVars_DrainPercent = 0.8f;
            bool nextBuffVars_DrainedBool = false;
            AddBuff(attacker, attacker, new Buffs.GlobalDrain(nextBuffVars_DrainPercent, nextBuffVars_DrainedBool), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target is Champion)
            {
                float temp1 = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                float maxHealth = effect0[level - 1];
                float percentDamage = temp1 * maxHealth;
                float minDamage = effect1[level - 1];
                float damageToDeal = Math.Max(minDamage, percentDamage);
                ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
            else
            {
                ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
                SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class HungeringStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Wolfman_SeverArmor.dds",
        };
    }
}