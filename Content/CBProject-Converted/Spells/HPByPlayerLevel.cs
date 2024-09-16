namespace Buffs
{
    public class HPByPlayerLevel : BuffScript
    {
        float hPPerLevel;
        float maxPlayerLevel;
        float lastTimeExecuted;
        public HPByPlayerLevel(float hPPerLevel = default)
        {
            this.hPPerLevel = hPPerLevel;
        }
        public override void OnActivate()
        {
            //RequireVar(this.hPPerLevel);
            maxPlayerLevel = 0;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
            {
                int playerLevel = GetLevel(unit);
                if (playerLevel > maxPlayerLevel)
                {
                    maxPlayerLevel = playerLevel;
                }
            }
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, false))
            {
                float myHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if (myHealth >= 0.99f)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
                    {
                        int playerLevel = GetLevel(unit);
                        if (playerLevel > maxPlayerLevel)
                        {
                            maxPlayerLevel = playerLevel;
                        }
                    }
                }
            }
            float hPIncrease = hPPerLevel * maxPlayerLevel;
            IncFlatHPPoolMod(owner, hPIncrease);
        }
    }
}