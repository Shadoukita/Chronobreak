namespace CharScripts
{
    public class CharScriptWolf : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 16.5f;
            float nextBuffVars_damagePerMinute = 0.286f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_goldPerMinute = 0.054f;
            float nextBuffVars_expPerMinute = 0.2136f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPermanentGoldReward(owner, -3);
            IncPermanentExpReward(owner, -5);
        }
    }
}