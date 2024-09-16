namespace Buffs
{
    public class DragonApplicator : BuffScript
    {
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            float newDuration;
            TeamId teamID = GetTeamID_CS(attacker);
            foreach (Champion unit in GetChampions(teamID, default, true))
            {
                newDuration = 120;
                if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                {
                    newDuration *= 1.15f;
                }
                else
                {
                    if (GetBuffCountFromCaster(unit, unit, nameof(Buffs.Monsterbuffs2)) > 0)
                    {
                        newDuration *= 1.3f;
                    }
                }
                if (!IsDead(unit))
                {
                    AddBuff(unit, unit, new Buffs.CrestofCrushingWrath(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                }
            }
        }
    }
}