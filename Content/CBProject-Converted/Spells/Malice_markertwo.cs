namespace Buffs
{
    public class Malice_markertwo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.SilentKiller)) > 0)
                {
                    int level = GetSlotSpellLevel((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}