namespace CharScripts
{
    public class CharScriptOriannaBall : CharScript
    {
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Champion caster = GetChampionBySkinName("Orianna", teamID);
            AddBuff(caster, owner, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(caster, owner, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            Vector3 myPosition = GetUnitPosition(owner);
            Vector3 nextBuffVars_MyPosition = myPosition;
            AddBuff(owner, caster, new Buffs.OriannaBallTracker(nextBuffVars_MyPosition), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}