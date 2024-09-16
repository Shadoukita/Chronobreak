namespace Buffs
{
    public class LeviathanAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeviathanAura",
            BuffTextureName = "3138_Leviathan.dds",
        };
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.85f;
        }
    }
}