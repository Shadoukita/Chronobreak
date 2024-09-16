namespace Buffs
{
    public class ActionTimer2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnDeactivate(bool expired)
        {
            bool foundUnit = false;
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.Stealth), false))
            {
                foundUnit = false; //TODO: true?
                bool canSee = CanSeeTarget(owner, unit);
                if (canSee)
                {
                    FaceDirection(owner, unit.Position3D);
                    SpellCast((ObjAIBase)owner, unit, owner.Position3D, owner.Position3D, 3, SpellSlotType.SpellSlots, 1, false, false, false, false, false, false);
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.ActionTimer(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
            }
            if (!foundUnit)
            {
                AddBuff(attacker, target, new Buffs.ActionTimer(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}