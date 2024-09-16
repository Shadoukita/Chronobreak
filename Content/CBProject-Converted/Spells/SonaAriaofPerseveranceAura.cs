namespace Buffs
{
    public class SonaAriaofPerseveranceAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "SonaAriaofPerseveranceAura",
            BuffTextureName = "Sona_AriaofPerseveranceGold.dds",
        };
        EffectEmitter ariaAura;
        float lastTimeExecuted;
        int[] effect0 = { 7, 9, 11, 13, 15 };
        public override void OnActivate()
        {
            SpellEffectCreate(out ariaAura, out _, "SonaAriaofPer_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(ariaAura);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_ARMRBoost = effect0[level - 1];
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SonaAriaofPerseveranceAuraB(nextBuffVars_ARMRBoost), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}