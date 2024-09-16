namespace Buffs
{
    public class LeonaSunlightPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeonaSunlightPassive",
            BuffTextureName = "LeonaSunlight.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int lastLifesteal; // UNUSED
        float lastSunlightDamage;
        float lastTimeExecuted;
        int[] effect0 = { 20, 20, 35, 35, 50, 50, 65, 65, 80, 80, 95, 95, 110, 110, 125, 125, 140, 140 };
        public override void OnActivate()
        {
            lastLifesteal = 0;
            lastSunlightDamage = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float sunlightDamage = effect0[level - 1];
                if (sunlightDamage > lastSunlightDamage)
                {
                    lastSunlightDamage = sunlightDamage;
                    SetBuffToolTipVar(2, sunlightDamage);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeonaSolarBarrierTracker)) > 0 && target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}