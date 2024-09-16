namespace Buffs
{
    public class JackInTheBoxInternal : BuffScript
    {
        Vector3 targetPos;
        int bonusHealth;
        float fearDuration;
        public JackInTheBoxInternal(Vector3 targetPos = default, int bonusHealth = default, float fearDuration = default)
        {
            this.targetPos = targetPos;
            this.bonusHealth = bonusHealth;
            this.fearDuration = fearDuration;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.fearDuration);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = this.targetPos;
            int nextBuffVars_BonusHealth = bonusHealth; // UNUSED
            Minion other3 = SpawnMinion("Jack In The Box", "ShacoBox", "turret.lua", targetPos, teamID, false, false, true, false, false, false, 0, false, false, (Champion)attacker);
            float nextBuffVars_FearDuration = fearDuration;
            AddBuff(attacker, other3, new Buffs.JackInTheBox(nextBuffVars_FearDuration), 1, 1, 60, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}