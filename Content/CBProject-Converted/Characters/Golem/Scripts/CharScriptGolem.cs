namespace CharScripts
{
    public class CharScriptGolem : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 37;
            float nextBuffVars_damagePerMinute = 1.05f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_goldPerMinute = 0.38f;
            float nextBuffVars_expPerMinute = 1.55f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(attacker, attacker, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentFlatHPPoolMod(owner, 150);
            IncPermanentFlatPhysicalDamageMod(owner, 3);
            IncPermanentGoldReward(owner, 8);
            IncPermanentExpReward(owner, 24);
        }
    }
}