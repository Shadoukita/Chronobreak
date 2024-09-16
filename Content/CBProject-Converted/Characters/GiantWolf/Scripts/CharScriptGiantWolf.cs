namespace CharScripts
{
    public class CharScriptGiantWolf : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 31;
            float nextBuffVars_damagePerMinute = 0.44f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_goldPerMinute = 0.43f;
            float nextBuffVars_expPerMinute = 1.5f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentFlatPhysicalDamageMod(owner, 2);
            IncPermanentGoldReward(owner, 6);
            IncPermanentExpReward(owner, 14);
        }
    }
}