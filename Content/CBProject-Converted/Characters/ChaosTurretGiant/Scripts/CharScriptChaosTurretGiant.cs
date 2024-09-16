namespace CharScripts
{
    public class CharScriptChaosTurretGiant : CharScript
    {
        public override void OnActivate()
        {
            float nextBuffVars_StartDecay = 1200.1f;
            AddBuff(owner, owner, new Buffs.TurretPreBonus(nextBuffVars_StartDecay), 1, 1, 960, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 960);
            int nextBuffVars_BonusHealth = 250;
            int nextBuffVars_BubbleSize = 800;
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