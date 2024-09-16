namespace Spells
{
    public class RedCardPreAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellCast(owner, target, target.Position3D, target.Position3D, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), owner);
        }
    }
}
namespace Buffs
{
    public class RedCardPreAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Card_Red.troy", },
            BuffName = "Pick A Card Red",
            BuffTextureName = "Cardmaster_red.dds",
        };
    }
}