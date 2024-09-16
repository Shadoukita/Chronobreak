namespace CharScripts
{
    public class CharScriptLesserWraith : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 12.1f;
            float nextBuffVars_damagePerMinute = 0.2195f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_goldPerMinute = 0.036f;
            float nextBuffVars_expPerMinute = 0.1064f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPermanentExpReward(owner, -11);
        }
    }
}