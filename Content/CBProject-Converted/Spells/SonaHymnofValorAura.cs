namespace Buffs
{
    public class SonaHymnofValorAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaHymnofValorAura",
            BuffTextureName = "Sona_HymnofValorGold.dds",
        };
        EffectEmitter hymnAura;
        float lastTimeExecuted;
        int[] effect0 = { 8, 11, 14, 17, 20 };
        public override void OnActivate()
        {
            SpellEffectCreate(out hymnAura, out _, "SonaHymnofValor_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(hymnAura);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_APADBoost = effect0[level - 1];
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SonaHymnofValorAuraB(nextBuffVars_APADBoost), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}