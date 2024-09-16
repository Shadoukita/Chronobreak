namespace Buffs
{
    public class LuxPrismaFieldStealth : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "LuxPrismWrap",
            BuffTextureName = "LuxPrismaWrap.dds",
        };
        bool willRemove;
        EffectEmitter akaliStealth;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.willRemove);
            SpellEffectCreate(out akaliStealth, out _, "akali_twilight_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            Fade iD = PushCharacterFade(owner, 0.2f, 0); // UNUSED
            SetStealthed(owner, true);
            SetGhosted(owner, true);
            willRemove = false;
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            SetGhosted(owner, false);
            PushCharacterFade(owner, 1, 0);
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
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) > 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                willRemove = true;
            }
            else if (!spellVars.DoesntTriggerSpellCasts)
            {
                willRemove = true;
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (IsDead(owner))
            {
                willRemove = true;
            }
        }
        public override void OnLaunchAttack(AttackableUnit target)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}