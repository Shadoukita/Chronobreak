namespace Spells
{
    public class BlueCardPreattack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellCast(owner, target, target.Position3D, target.Position3D, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), owner);
        }
    }
}
namespace Buffs
{
    public class BlueCardPreattack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Card_Blue.troy", },
            BuffName = "Pick A Card Blue",
            BuffTextureName = "Cardmaster_blue.dds",
        };
    }
}