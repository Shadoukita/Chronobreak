namespace CharScripts
{
    public class CharScriptLizardElder : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_spawnTime = 101;
            float nextBuffVars_areaDmgReduction = 0.2f; // UNUSED
            float nextBuffVars_healthPerMinute = 90;
            float nextBuffVars_damagePerMinute = 1.825f;
            float nextBuffVars_goldPerMinute = 0.342f;
            float nextBuffVars_expPerMinute = 2.5f;
            bool nextBuffVars_upgradeTimer = true;
            AddBuff(owner, owner, new Buffs.GlobalMonsterBuff(nextBuffVars_spawnTime, nextBuffVars_healthPerMinute, nextBuffVars_damagePerMinute, nextBuffVars_goldPerMinute, nextBuffVars_expPerMinute, nextBuffVars_upgradeTimer), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.BlessingoftheLizardElder(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000, true, false, false);
            IncPermanentFlatHPPoolMod(owner, 100);
        }
    }
}