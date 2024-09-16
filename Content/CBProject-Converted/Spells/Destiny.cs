namespace Spells
{
    public class Destiny : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 150f, 135f, 120f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        int[] effect0 = { 6, 8, 10 };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.Destiny_marker(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.Destiny(), 1, 1, effect0[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.Gate));
            SetSlotSpellCooldownTimeVer2(0.5f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
        }
    }
}
namespace Buffs
{
    public class Destiny : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DestinyEye.troy", "", },
            BuffName = "Destiny",
            BuffTextureName = "Destiny_temp.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId casterID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(casterID, 1000, owner, 40, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}