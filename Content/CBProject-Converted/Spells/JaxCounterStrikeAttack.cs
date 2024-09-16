namespace Spells
{
    public class JaxCounterStrikeAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 40, 70, 100, 130, 160 };
        public override void SelfExecute()
        {
            PlayAnimation("Spell3B", 0, attacker, false, false, false);
            SpellEffectCreate(out _, out _, "Counterstrike_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDmg = effect0[level - 1];
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            bonusAD *= 0.8f;
            float damage = bonusAD + baseDmg;
            damage += charVars.NumCounter;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            ApplyStun(attacker, target, 1);
        }
    }
}
namespace Buffs
{
    public class JaxCounterStrikeAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CounterStrike",
            BuffTextureName = "Armsmaster_Disarm.dds",
            IsDeathRecapSource = true,
        };
    }
}