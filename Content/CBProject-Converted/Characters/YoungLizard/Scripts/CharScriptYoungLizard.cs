namespace CharScripts
{
    public class CharScriptYoungLizard : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 11.88f;
            float nextBuffVars_damagePerMinute = 0.22f;
            float nextBuffVars_goldPerMinute = 0.05f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_expPerMinute = 0.424f;
            bool nextBuffVars_upgradeTimer = true;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}