namespace CharScripts
{
    public class CharScriptChaosTurretShrine : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.TurretBonus(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 60);
            float nextBuffVars_BonusHealth = 0;
            float nextBuffVars_BubbleSize = 1600;
            AddBuff(owner, owner, new Buffs.TurretBonusHealth(nextBuffVars_BonusHealth, nextBuffVars_BubbleSize), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}