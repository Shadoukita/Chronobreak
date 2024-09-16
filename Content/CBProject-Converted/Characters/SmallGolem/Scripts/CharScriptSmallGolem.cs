namespace CharScripts
{
    public class CharScriptSmallGolem : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 25;
            float nextBuffVars_damagePerMinute = 0.84f;
            float nextBuffVars_goldPerMinute = 0.15f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_expPerMinute = 1.08f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(attacker, attacker, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPermanentFlatHPPoolMod(owner, -150);
            IncPermanentFlatPhysicalDamageMod(owner, -3);
            IncPermanentGoldReward(owner, -8);
            IncPermanentExpReward(owner, -12);
        }
    }
}