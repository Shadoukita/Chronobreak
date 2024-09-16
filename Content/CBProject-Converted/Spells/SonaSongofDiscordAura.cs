namespace Buffs
{
    public class SonaSongofDiscordAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaSongofDiscordAura",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
        };
        EffectEmitter songAura;
        float lastTimeExecuted;
        int[] effect0 = { 8, 11, 14, 17, 20 };
        public override void OnActivate()
        {
            SpellEffectCreate(out songAura, out _, "SonaSongofDiscord_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(songAura);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MSBoost = effect0[level - 1];
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SonaSongofDiscordAuraB(nextBuffVars_MSBoost), 1, 1, 0.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
    }
}