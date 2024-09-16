namespace Buffs
{
    public class AkaliSmokeBombInternal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        bool willFade;
        EffectEmitter abc;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            willFade = true;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
            {
                Fade iD = PushCharacterFade(owner, 0.2f, 1.5f); // UNUSED
                willFade = false;
                SpellEffectCreate(out abc, out _, "akali_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove = false;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Recall)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliSBStealth(nextBuffVars_WillRemove), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            }
            if (!willFade)
            {
                SpellEffectRemove(abc);
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (IsDead(owner))
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}