namespace CharScripts
{
    public class CharScriptWraith : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.LifestealAttack(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_healthPerMinute = 28;
            float nextBuffVars_damagePerMinute = 0.5f;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_goldPerMinute = 0.48f;
            float nextBuffVars_expPerMinute = 1.4f;
            bool nextBuffVars_upgradeTimer = false;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RegenerationRuneAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPermanentExpReward(owner, 33);
            IncPermanentFlatPhysicalDamageMod(owner, 1);
        }
    }
}