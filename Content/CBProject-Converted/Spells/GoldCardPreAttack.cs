namespace Spells
{
    public class GoldCardPreAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellCast(owner, target, target.Position3D, target.Position3D, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), owner);
        }
    }
}
namespace Buffs
{
    public class GoldCardPreAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Card_Yellow.troy", },
            BuffName = "Pick A Card Gold",
            BuffTextureName = "Cardmaster_gold.dds",
        };
    }
}