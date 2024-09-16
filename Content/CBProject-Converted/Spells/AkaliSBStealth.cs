namespace Buffs
{
    public class AkaliSBStealth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AkaliTwilightShroud",
            BuffTextureName = "AkaliTwilightShroud.dds",
        };
        EffectEmitter akaliStealth;
        bool willRemove;
        float moveSpeedBuff;
        int[] effect0 = { 0, 0, 0, 0, 0 };
        public AkaliSBStealth(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out akaliStealth, out _, "akali_twilight_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            Fade iD = PushCharacterFade(owner, 0.2f, 0); // UNUSED
            SetStealthed(owner, true);
            SetGhosted(owner, true);
            willRemove = false;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            moveSpeedBuff = effect0[level - 1];
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliHoldStealth(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            SpellEffectRemove(akaliStealth);
        }
        public override void OnUpdateStats()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
            IncPercentMovementSpeedMod(owner, moveSpeedBuff);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellName != nameof(Spells.ShadowWalk))
            {
                if (spellVars.CastingBreaksStealth)
                {
                    willRemove = true;
                }
                else if (!spellVars.CastingBreaksStealth)
                {
                }
                else if (!spellVars.DoesntTriggerSpellCasts)
                {
                    willRemove = true;
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (IsDead(owner))
            {
                willRemove = true;
            }
        }
    }
}