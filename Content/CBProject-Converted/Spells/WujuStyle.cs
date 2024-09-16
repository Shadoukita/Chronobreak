namespace Spells
{
    public class WujuStyle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.WujuStyle), owner);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, owner, new Buffs.WujuStyleSuperCharged(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class WujuStyle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weaponstreak", },
            AutoBuffActivateEffect = new[] { "Wujustyle_buf.troy", },
            BuffName = "Wuju Style",
            BuffTextureName = "MasterYi_SunderingStrikes.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            SpellBuffRemove(owner, nameof(Buffs.MasterYiWujuDeactivated), (ObjAIBase)owner);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = 10;
            float levelDamage = 5 * level;
            float totalDamage = levelDamage + baseDamage;
            IncFlatPhysicalDamageMod(owner, totalDamage);
        }
    }
}