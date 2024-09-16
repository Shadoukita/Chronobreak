namespace CharScripts
{
    public class CharScriptChaosTurretWorm : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.TurretBonus(), 1, 1, 480.1f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60);
            float nextBuffVars_BonusHealth = 250;
            float nextBuffVars_BubbleSize = 800;
            AddBuff(owner, owner, new Buffs.TurretBonusHealth(nextBuffVars_BonusHealth, nextBuffVars_BubbleSize), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.TurretChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 10);
            AddBuff(owner, owner, new Buffs.TurretAssistManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1);
            AddBuff(owner, owner, new Buffs.TurretDamageManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1);
            SetDodgePiercing(owner, true);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.2f);
        }
    }
}