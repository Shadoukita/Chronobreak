namespace Buffs
{
    public class GlobalMonsterBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        float spawnTime;
        float healthPerMinute;
        float damagePerMinute;
        float goldPerMinute;
        float expPerMinute;
        bool upgradeTimer;
        float lastTimeExecuted;
        public GlobalMonsterBuff(float spawnTime = default, float healthPerMinute = default, float damagePerMinute = default, float goldPerMinute = default, float expPerMinute = default, bool upgradeTimer = default)
        {
            this.spawnTime = spawnTime;
            this.healthPerMinute = healthPerMinute;
            this.damagePerMinute = damagePerMinute;
            this.goldPerMinute = goldPerMinute;
            this.expPerMinute = expPerMinute;
            this.upgradeTimer = upgradeTimer;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spawnTime);
            //RequireVar(this.healthPerMinute);
            //RequireVar(this.damagePerMinute);
            //RequireVar(this.goldPerMinute);
            //RequireVar(this.expPerMinute);
            //RequireVar(this.upgradeTimer);
            float gameTime = GetGameTime();
            float jungleLifeTime = gameTime - spawnTime;
            jungleLifeTime = Math.Max(jungleLifeTime, 0);
            float bonusHealth = jungleLifeTime * healthPerMinute;
            bonusHealth /= 60;
            float bonusDamage = jungleLifeTime * damagePerMinute;
            bonusDamage /= 60;
            float bonusGold = jungleLifeTime * goldPerMinute;
            bonusGold /= 60;
            float bonusExp = jungleLifeTime * expPerMinute;
            bonusExp /= 60;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
            IncPermanentExpReward(owner, bonusExp);
            IncPermanentGoldReward(owner, bonusGold);
        }
        public override void OnUpdateStats()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent >= 0.995f && upgradeTimer && ExecutePeriodically(60, ref lastTimeExecuted, false))
            {
                IncPermanentFlatHPPoolMod(owner, healthPerMinute);
                IncPermanentFlatPhysicalDamageMod(owner, damagePerMinute);
                IncPermanentExpReward(owner, expPerMinute);
                IncPermanentGoldReward(owner, goldPerMinute);
            }
        }
    }
}