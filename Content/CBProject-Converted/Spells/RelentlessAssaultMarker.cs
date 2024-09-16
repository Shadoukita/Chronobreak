namespace Buffs
{
    public class RelentlessAssaultMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Armsmaster_CoupDeGrace.dds",
        };
        bool isActive;
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, level, false);
            isActive = false;
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Empower)) > 0)
            {
                OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                RemoveOverrideAutoAttack(owner, false);
            }
        }
        public override void OnUpdateStats()
        {
            isActive = true;
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (isActive)
            {
                SpellBuffRemove(owner, nameof(Buffs.RelentlessAssaultMarker), (ObjAIBase)owner);
                SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.RelentlessAssaultDebuff), 0);
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is not ObjAIBase)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if (target is BaseTurret)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}