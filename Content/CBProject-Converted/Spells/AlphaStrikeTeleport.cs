namespace Spells
{
    public class AlphaStrikeTeleport : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.AlphaStrikeMarker), true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.AlphaStrikeMarker)) > 0)
                {
                    Vector3 pos = GetPointByUnitFacingOffset(unit, 75, 0);
                    TeleportToPosition(owner, pos);
                    if (unit is Champion)
                    {
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.AlphaStrikeMarker), owner, 0);
                    SpellBuffRemove(owner, nameof(Buffs.AlphaStrikeMarker), owner, 0);
                }
            }
        }
    }
}