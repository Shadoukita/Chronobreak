namespace Buffs
{
    public class SkarnerBrushCheck : BuffScript
    {
        float brushChecks;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            brushChecks = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(3, ref lastTimeExecuted, false))
            {
                bool brushCheck = IsInBrush(owner);
                if (brushCheck)
                {
                    if (brushChecks == 12)
                    {
                        if (RandomChance() < 0.05f)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.SkarnerBrushSound(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                            SpellBuffClear(owner, nameof(Buffs.SkarnerBrushCheck));
                        }
                        else
                        {
                            brushChecks = 0;
                        }
                    }
                    else
                    {
                        brushChecks += 3;
                    }
                }
                else
                {
                    brushChecks = 0;
                }
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            brushChecks = 0;
        }
    }
}