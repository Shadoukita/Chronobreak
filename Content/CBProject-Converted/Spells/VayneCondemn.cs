namespace Spells
{
    public class VayneCondemn : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            charVars.CastPoint = GetUnitPosition(owner);
            SpellCast(owner, target, target.Position3D, target.Position3D, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class VayneCondemn : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
    }
}