namespace Buffs
{
    public class PropelSpellCaster : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "PropelGeyser.troy", },
        };
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.Propel));
            SpellCast((ObjAIBase)owner, target, owner.Position3D, owner.Position3D, 0, SpellSlotType.ExtraSlots, 1, true, false, false, false, false, false);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
    }
}