namespace Spells
{
    public class SeismicShard : SpellScript
    {
        int[] effect0 = { 70, 120, 170, 220, 270 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_Level = level;
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0.6f, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.SeismicShardBuff(nextBuffVars_Level), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class SeismicShard : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SeismicShard",
            BuffTextureName = "Malphite_SeismicShard.dds",
        };
    }
}