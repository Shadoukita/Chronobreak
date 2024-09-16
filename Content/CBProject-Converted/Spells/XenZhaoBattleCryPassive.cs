namespace Spells
{
    public class XenZhaoBattleCryPassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class XenZhaoBattleCryPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "XenZhaoBattleCryPassive",
            BuffTextureName = "XenZhao_BattleCry.dds",
        };
        EffectEmitter passivePart;
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void OnActivate()
        {
            SpellEffectCreate(out passivePart, out _, "xen_ziou_battleCry_passive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(passivePart);
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float aSAura = effect0[level - 1];
            IncPercentAttackSpeedMod(owner, aSAura);
        }
    }
}