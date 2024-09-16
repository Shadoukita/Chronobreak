namespace Spells
{
    public class InfectedCleaverMissileCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "MundoMundo", },
            SpellVOOverrideSkins = new[] { "CorporateMundo", },
        };
        int[] effect0 = { 50, 60, 70, 80, 90 };
        public override bool CanCast()
        {
            bool returnValue = true;
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int healthCost = effect0[level - 1];
            float health = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (health <= healthCost)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 1000)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 850, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}